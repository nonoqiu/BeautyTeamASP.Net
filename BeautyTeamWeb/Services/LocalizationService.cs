using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeautyTeamWeb
{
    public class ObisoftLocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Language = filterContext.RouteData.Values["lang"].ToString();
            if (string.IsNullOrWhiteSpace(Language))
            {
                try
                {
                    Language = filterContext.HttpContext.Request.UserLanguages[0].Split(';')[0];
                }
                catch
                {
                    Language = "en-US";
                }
                filterContext.RouteData.Values["lang"] = Language;
            }
            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Language);
            }
            catch (CultureNotFoundException)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
            }
        }
    }
    public static class ObiLocalizationLibrary
    {
        public static RouteValueDictionary ChangeAndReturn(this RouteValueDictionary source, string key, string value)
        {
            RouteValueDictionary rDic = new RouteValueDictionary();
            foreach (var s in source)
            {
                rDic.Add(s.Key, s.Value);
            }
            rDic[key] = value;
            return rDic;
        }
    }
}