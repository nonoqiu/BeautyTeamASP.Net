using BeautyTeamWeb.ViewModels;
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
            return await Task.Factory.StartNew(() =>
            {
                var model = new HomeIndexViewModel
                {
                    Users = DbContext.Users.Count(),
                    GroupNumbers = DbContext.Groups.Count(),
                    Projects = DbContext.Projects.Count(),
                    Tasks = DbContext.GroupTasks.Count() + DbContext.PersonalTasks.Count() + DbContext.RadioTasks.Count()
                };
                return View(model);
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> SubScribe(SubScribeViewModel model)
        {
            if (ModelState.IsValid)
            {
                DbContext.EmailSubscribes.Add(new Models.EmailSubscribe
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