using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyTeamWeb.Controllers
{
    public class PortfolioController : ControllerWithAuthorize
    {
        // GET: Portfolio
        [ObisoftLocalization]
        [RequireHttps]
        public ActionResult HSharp()
        {
            return View(DbContext.Products.Find(1));
        }
    }
}