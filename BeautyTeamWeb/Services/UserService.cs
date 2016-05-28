using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using BeautyTeamWeb.Models;
using System.Web.Mvc;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace BeautyTeamWeb
{
    public class ObisoftUserManager : UserManager<ObisoftUser>
    {
        public ObisoftUserManager(IUserStore<ObisoftUser> store) : base(store)
        {
        }
    }

    public class ObisoftSignInManager : SignInManager<ObisoftUser, string>
    {
        public ObisoftSignInManager(ObisoftUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ObisoftUser user) =>
        user.GenerateUserIdentityAsync((ObisoftUserManager)UserManager);
    }
    public class ObisoftAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            filterContext.HttpContext.Response.Redirect($"/{filterContext.RouteData.Values["lang"]}/Account/Login?ReturnURL={HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl)}");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }

    public class ControllerWithAuthorize : Controller
    {
        private ObisoftSignInManager _signInManager;
        private ObisoftUserManager _userManager;
        public ObisoftUser user() => UserManager.FindById(User.Identity.GetUserId());
        public async Task<ObisoftUser> userAsync() => await UserManager.FindByIdAsync(User.Identity.GetUserId());
        public ObisoftUser user(string Email) => UserManager.FindByEmail(Email);

        public static string OkResult = _Json(new ServerReply { StatusCode = HttpStatusCode.OK });
        public static string ConflictSetResult = _Json(new ServerReply { StatusCode = HttpStatusCode.Conflict });
        public static string ErrorResult = _Json(new ServerReply { StatusCode = HttpStatusCode.InternalServerError });
        public static string NotAcceptableResult = _Json(new ServerReply { StatusCode = HttpStatusCode.NotAcceptable });
        public static string NotFoundResult = _Json(new ServerReply { StatusCode = HttpStatusCode.NotFound });
        public static string ForbiddenResult = _Json(new ServerReply { StatusCode = HttpStatusCode.Forbidden });

        public static string _Json(object data) => JsonConvert.SerializeObject(data);
        public static async Task<string> MessageAsync(string content, HttpStatusCode Code) => await _JsongAsync(new ObiObject<string> { Object = content, StatusCode = Code });
        public static async Task<string> _JsongAsync(object data)
        {
            return await System.Threading.Tasks.Task.Factory.StartNew(() => JsonConvert.SerializeObject(data));
        }
        public ControllerWithAuthorize() { }

        public ControllerWithAuthorize(ObisoftUserManager u, ObisoftSignInManager s)
        {
            UserManager = u;
            SignInManager = s;
        }

        public ObisoftSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ObisoftSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ObisoftUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ObisoftUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public BeautyTeamDbContext DbContext = new BeautyTeamDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
                _signInManager.Dispose();
                _signInManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
