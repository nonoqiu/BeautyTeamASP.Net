using BeautyTeamWeb.ViewModels;
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

        public ActionResult TheLightOfTheory()
        {
            return View();
        }

        public async Task<ActionResult> AllJobs(JobType? id)
        {
            if (id == null)
            {
                var Jobs = await DbContext.JobOpenings.ToListAsync();
                return View(Jobs);
            }
            else
            {
                var Jobs = await DbContext.JobOpenings.Where(t=>t.JobType==id).ToListAsync();
                return View(Jobs);
            }
        }

        public async Task<ActionResult> JobDetails(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("AllJobs");
            }
            var CurrentJob = await DbContext.JobOpenings.FindAsync(id);
            if(CurrentJob==null)
            {
                return RedirectToAction("AllJobs");
            }
            return View(CurrentJob);
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