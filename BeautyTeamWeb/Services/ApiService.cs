using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeautyTeamWeb
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect($"/api/_Unauthorized");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
    public static class ApiControllerTool
    {
        public async static Task<string> _apiReplyTool(this ControllerWithAuthorize c, Func<Task<string>> method)
        {
            c.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:9000");
            c.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            c.Response.ContentType = "application/json";
            if (!c.ModelState.IsValid)
            {
                return ControllerWithAuthorize.NotAcceptableResult;
            }
            try
            {
                return await method();
            }
            catch (Exception e)
            {
                try
                {
                    c.DbContext.EventLogDbs.Add(new Models.EventLogDb
                    {
                        Description = c.Request.RawUrl + e.Message,
                        HappenPlatform = Models.Platform.Web,
                        HappenTime = DateTime.Now,
                        version = "WebVersion"
                    });
                    await c.DbContext.SaveChangesAsync();
                }
                catch
                {
                }
                return ControllerWithAuthorize.ErrorResult;
            }
        }
    }
}