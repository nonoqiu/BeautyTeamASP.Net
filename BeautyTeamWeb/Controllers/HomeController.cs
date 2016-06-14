﻿using BeautyTeamWeb.ViewModels;
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
                Newss = await DbContext.NewsViewModels.OrderByDescending(t => t.PublishTime).ToListAsync()
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



        #region Job
        public async Task<ActionResult> AllJobs(JobType? id)
        {
            if (id == null)
            {
                var Jobs = await DbContext.JobOpenings.ToListAsync();
                return View(Jobs);
            }
            else
            {
                var Jobs = await DbContext.JobOpenings.Where(t => t.JobType == id).ToListAsync();
                return View(Jobs);
            }
        }
        public async Task<ActionResult> JobDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllJobs");
            }
            var CurrentJob = await DbContext.JobOpenings.FindAsync(id);
            if (CurrentJob == null)
            {
                return RedirectToAction("AllJobs");
            }
            CurrentJob.Views++;
            await DbContext.SaveChangesAsync();

            var Suggestion = await DbContext.JobOpenings.OrderByDescending(t => t.Views).ToListAsync();
            ViewBag.Suggestion = Suggestion;
            return View(CurrentJob);
        }
        [ObisoftAuthorize]
        public async Task<ActionResult> ApplyJob(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllJobs");
            }
            var CurrentJob = await DbContext.JobOpenings.FindAsync(id);
            if (CurrentJob == null)
            {
                return RedirectToAction("AllJobs");
            }
            return View(CurrentJob);
        }
        [HttpPost]
        public async Task<ActionResult> ApplyJob(ApplyJobModel model)
        {
            if (ModelState.IsValid)
            {
                DbContext.UserApplyJobModels.Add(new UserApplyJobModel
                {
                    Email = model.Email,
                    JobId = model.JobId,
                    Message = model.Message,
                    Name = model.Name,
                    ObisoftUserId = User.Identity.GetUserId(),
                    Phone = model.Phone
                });
                await DbContext.SaveChangesAsync();
                return View("Applied");
            }
            return RedirectToAction("JobDetails", new { id = model.JobId });
        }
        #endregion

        [ObisoftAuthorize]
        public ActionResult Applied()
        {
            return View();
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