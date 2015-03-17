using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using virtualtri.Entities;
using virtualtri.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;
using virtualtri.Properties;

namespace virtualtri.Controllers
{
    [Authorize]
    public class MyActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /MyActivities/
        public ActionResult Index()
        {
            ViewBag.TeamName = Settings.Default.team_name;
            // based on the current user, let's setup the model
            var userActivities = new MyActivitiesViewModel();
            userActivities.Participant = db.Users.Find(User.Identity.GetUserId());

            userActivities.NewActivity = new Activity()
            {
                ActivityDateTime = DateTime.Now
            };

            string id = User.Identity.GetUserId();

            // get the user's info
            var activities = (from a in db.Activities
                              where a.ApplicationUser_Id == id
                              select a)
                             .OrderByDescending(a => a.ActivityDateTime);
                             
            userActivities.Activities = activities == null ? new List<Activity>() : activities.ToList();

            userActivities.TotalDistance = userActivities.Activities.Sum(a => a.Distance);
            userActivities.PercentComplete = Math.Floor((userActivities.TotalDistance / 140.6)*100);

            if (userActivities.PercentComplete > 100)
            {
                userActivities.PercentComplete = 100;
            }

            return View(userActivities);
        }

        //
        // POST: /MyActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ActivityType,Distance,ActivityDateTime")] Activity activity)
        {
            try
            {
                // we need to see how many miles the user currently has
                string id = User.Identity.GetUserId();

                // get the user's info
                var activities = (from a in db.Activities
                                  where a.ApplicationUser_Id == id
                                  select a)
                                 .OrderByDescending(a => a.ActivityDateTime);

                if ((activities != null) && (activities.Count() > 0))
                {
                    var totalDistance = activities.Sum(a => a.Distance);

                    // if this entry puts the user over 140.6, we need to update the "date completed" record
                    if ((totalDistance < 140.6) && (totalDistance + activity.Distance >= 140.6))
                    {
                        ApplicationUser appUser = db.Users.Find(User.Identity.GetUserId());
                        appUser.DateCompleted = activity.ActivityDateTime;
                        db.Entry(appUser).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }

                activity.ApplicationUser_Id = User.Identity.GetUserId();

                db.Activities.Add(activity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //return Content(ex.Message);
                return RedirectToAction("Index");
                //return View();
            }
        }

        // GET: /MyActivities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.TeamName = Settings.Default.team_name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }

            if (activity.ApplicationUser_Id == User.Identity.GetUserId())
            {
                return View(activity);
            }
            else
            {
                return Redirect("/error.html");
            }
        }

        // POST: /MyActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activities.FindAsync(id);
            db.Activities.Remove(activity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
