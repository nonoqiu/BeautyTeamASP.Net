using BeautyTeamWeb.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeautyTeamWeb.Controllers
{
    [ObisoftLocalization]
    [RequireHttps]
    public class HomeController : ControllerWithAuthorize
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                Users = DbContext.Users.Count(),
                GroupNumbers = DbContext.Groups.Count(),
                Projects = DbContext.Projects.Count(),
                Tasks = DbContext.GroupTasks.Count() + DbContext.PersonalTasks.Count() + DbContext.RadioTasks.Count(),
                Newss = await DbContext.NewsViewModels.OrderByDescending(t => t.PublishTime).ToListAsync(),
                Types = await DbContext.ProductTypes.ToListAsync()
            };
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Careers()
        {
            return View();
        }
        public ActionResult PrivacyStatement()
        {
            return View();
        }
        public ActionResult APIDoc()
        {
            return View();
        }

        public ActionResult Light()
        {
            return View(new LightOfTheory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Light(LightOfTheory model)
        {
            if (ModelState.IsValid)
            {
                DbContext.LightOfTheorys.Add(model);
                await DbContext.SaveChangesAsync();
                return RedirectToAction("LightApplied");
            }
            return View(model);
        }

        public ActionResult LightApplied()
        {
            return View();
        }
        public async Task<ActionResult> AllNews()
        {
            var model = new HomeIndexViewModel
            {
                Newss = await DbContext.NewsViewModels.OrderByDescending(t => t.PublishTime).ToListAsync()
            };
            return View("_Blog", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> SubScribe(SubScribeViewModel model)
        {
            if (ModelState.IsValid)
            {
                DbContext.EmailSubscribes.Add(new EmailSubscribe
                {
                    Email = model.Email
                });
                await DbContext.SaveChangesAsync();
            }
            return PartialView("_Thanks");
        }

        public ActionResult Error()
        {
            return View("Error");
        }
    }
}