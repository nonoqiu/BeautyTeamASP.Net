using BeautyTeamWeb.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeautyTeamWeb.Controllers
{
    [ObisoftLocalization]
    [RequireHttps]
    public class JobController : ControllerWithAuthorize
    {
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

        [ObisoftAuthorize]
        public ActionResult Applied()
        {
            return View();
        }
    }
}