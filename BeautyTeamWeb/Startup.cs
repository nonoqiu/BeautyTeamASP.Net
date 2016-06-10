using BeautyTeamWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
#region Owin
using Owin.Security.Providers.BattleNet;
using Owin.Security.Providers.Fitbit;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.Instagram;
using Owin.Security.Providers.PayPal;
using Owin.Security.Providers.Spotify;
using Owin.Security.Providers.Dropbox;
using Owin.Security.Providers.Flickr;
using Owin.Security.Providers.GooglePlus;
using Owin.Security.Providers.HealthGraph;
using Owin.Security.Providers.LinkedIn;
using Owin.Security.Providers.Vimeo;
using Owin.Security.Providers.VisualStudio;
using Owin.Security.Providers.Yahoo;
using Owin.Security.Providers.Twitch;
using Owin.Security.Providers.Steam;
#endregion
using Owin;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BeautyTeamWeb.Services;
using static System.Web.Mvc.AreaRegistration;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity.Migrations;

[assembly: OwinStartupAttribute(typeof(BeautyTeamWeb.Startup))]
namespace BeautyTeamWeb
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        public static RouteCollection RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("api", "api/{action}/{id}",
            new { lang = "en-US", controller = "api", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Localization", "{lang}/{controller}/{action}/{id}",
            new { lang = string.Empty, controller = "Home", action = "Index", id = UrlParameter.Optional });

            return routes;
        }
        public static void RegisterBundles(BundleCollection bundles)
        {
            string Webroot = "~/wwwroot/";
            //Responsive CSS
            bundles.Add(new StyleBundle("~/css").Include(
            Webroot + "/css/bootstrap.css",
            Webroot + "/css/SiteColor.css",
            Webroot + "/css/Site.css",
            Webroot + "/css/Sitefont.css"
            ));
            //Responsive JS
            bundles.Add(new ScriptBundle("~/js").Include(
            Webroot + "/js/jquery-{version}.js",
            Webroot + "/js/bootstrap.js",
            Webroot + "/js/customBack.js",
            Webroot + "/js/jquery.unobtrusive-ajax.js"
            ));
            //Validate, Place only when ajax and validate required!
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            Webroot + "/js/jquery.validate.js",
            Webroot + "/js/jquery.validate.unobtrusive.js"
            ));
            //HomePage required, Place only in homepage.
            bundles.Add(new StyleBundle("~/css/ForHome").Include(
            Webroot + "/css/OwlCarousel/owl.carousel.css",
            Webroot + "/css/OwlCarousel/owl.theme.css",
            Webroot + "/css/prettyPhoto.css"
            ));
            //HomePage required, Place only in homepage.
            bundles.Add(new ScriptBundle("~/js/ForHome").Include(
            Webroot + "/js/owl.carousel.js",
            Webroot + "/js/jquery.isotope.js",
            Webroot + "/js/jquery.prettyPhoto.js",
            Webroot + "/js/jquery.singlePageNav.js",
            Webroot + "/js/smooth-scroll.js",
            Webroot + "/js/jquery.fancybox.pack.js",
            Webroot + "/js/jquery.bxslider.min.js",
            Webroot + "/js/jquery.counterup.min.js",
            Webroot + "/js/jquery.easing.1.3.js",
            Webroot + "/js/jquery.scrollTo.js",
            Webroot + "/js/waypoints.min.js",
            Webroot + "/js/wow.min.js",
            Webroot + "/js/custom.js"
            ));
            //WeChat UI
            bundles.Add(new StyleBundle("~/css/WeUI").Include(
            Webroot + "/css/weui.min.css",
            Webroot + "/css/example.css"));
        }
        public static ObisoftUserManager CreateUserManager(IdentityFactoryOptions<ObisoftUserManager> options, IOwinContext context)
        {
            var manager = new ObisoftUserManager(new UserStore<ObisoftUser>(context.Get<BeautyTeamDbContext>()))
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                },
                UserLockoutEnabledByDefault = true,
                DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5),
                MaxFailedAccessAttemptsBeforeLockout = 5,
                EmailService = new EmailService(),
                SmsService = new SmsService(),
            };
            manager.UserValidator = new UserValidator<ObisoftUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ObisoftUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ObisoftUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                new DataProtectorTokenProvider<ObisoftUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
        public static ObisoftSignInManager CreateSignInManager(IdentityFactoryOptions<ObisoftSignInManager> options, IOwinContext context)
        {
            return new ObisoftSignInManager(context.GetUserManager<ObisoftUserManager>(), context.Authentication);
        }
        public static CookieAuthenticationOptions CookieAuth()
        {
            var Cookie = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/en-US/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ObisoftUserManager, ObisoftUser>(
            validateInterval: TimeSpan.FromMinutes(30),
            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            };
            return Cookie;
        }
        public void Configuration(IAppBuilder app)
        {
            string waitting = string.Empty;
            app.CreatePerOwinContext(() => { return new BeautyTeamDbContext(); });
            app.CreatePerOwinContext<ObisoftUserManager>(CreateUserManager);
            app.CreatePerOwinContext<ObisoftSignInManager>(CreateSignInManager);

            app.UseCookieAuthentication(CookieAuth());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //app.UseMicrosoftAccountAuthentication("000000004C191545", "BOhdMxdyVPdsTH8qZLyiiqU");
            //app.UseFacebookAuthentication("1755093041403342", "14745cd185959ac9d19d75b079592c6e");
            //app.UseTwitterAuthentication("1usofmLObsFymH7hUo6sF6DFi", "T7KiA4HxH4LRsgYFAFFsEpLRYQWnKj7DzXy4cx6OeNVviSzdB6");
            app.UseGitHubAuthentication("a2921a92d97e3d903fe4", "881bb2326621323230b36c2ad32f895a6b26a183");
            //    app.UseVisualStudioAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseInstagramInAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseHealthGraphAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseGooglePlusAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseBattleNetAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseLinkedInAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseSpotifyAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseDropboxAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseFitbitAuthentication(nameof(waitting), nameof(waitting));
            //    app.UsePayPalAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseFlickrAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseGoogleAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseTwitchAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseVimeoAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseYahooAuthentication(nameof(waitting), nameof(waitting));
            //    app.UseSteamAuthentication(nameof(waitting));
        }
    }
}
