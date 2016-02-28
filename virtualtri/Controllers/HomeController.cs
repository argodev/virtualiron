using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using virtualtri.Models;
using Microsoft.AspNet.Identity;
using virtualtri.Entities;
using virtualtri.Properties;

namespace virtualtri.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.TeamName = Settings.Default.team_name;

            // based on the current user, let's setup the model
            var allActivities = new HomePageModel();

            allActivities.TeamName = Settings.Default.team_name;
            allActivities.StartDate = Settings.Default.start_date;
            allActivities.EndDate = Settings.Default.end_date;
            allActivities.TargetEventName = Settings.Default.target_event_name;
            allActivities.TargetEventDescription = Settings.Default.target_event_description;
            allActivities.TargetEventLink = Settings.Default.target_event_link;

            //if (User.Identity.IsAuthenticated)
            //{

            //}
            var users = (from row in db.Users
                        select new { row.Id, row.UserName }).ToList();

            List<UserSummary> result =
            (
                from row in db.Activities
                group row by new { row.ApplicationUser_Id } into g
                select new UserSummary()
                {
                    ApplicationUser_Id = g.Key.ApplicationUser_Id,
                    TotalDistance = g.Sum(x => x.Distance)
                }
            ).OrderByDescending(r => r.TotalDistance).ToList();

            // now we need to loop through it all...
            result.Select(r => 
            {
                // now we need to add the username as well as 
                r.DisplayName = (from x in users where x.Id == r.ApplicationUser_Id select x.UserName).FirstOrDefault();

                r.TargetDistance = db.Users.Where(u => u.Id == r.ApplicationUser_Id).Select(u => u.TargetDistance).FirstOrDefault();

                // calculate the percent complete
                r.PercentComplete = r.TotalDistance >= r.TargetDistance ? 100 : Math.Floor((r.TotalDistance / r.TargetDistance) * 100);
                return r;
            }).ToList();


            allActivities.SummaryData = result;

            // calculate the team totals
            // 1. how many team members are there
            var count = db.Users.Count();

            // 2. total miles
            int teamTotalGoal = 0;
            try
            {
                db.Users.Select(u => u.TargetDistance).Sum();
            } catch (System.InvalidOperationException)
            {
                // this is likely due to no one being in the db
                teamTotalGoal = 0;
            }

            // 3. actual total miles
            var teamActualMiles = result.Sum(a => a.TotalDistance);
            allActivities.TeamTotalDistance = teamActualMiles;
            allActivities.TeamPercentComplete = teamActualMiles >= teamTotalGoal ? 100 : Math.Floor((teamActualMiles / teamTotalGoal) * 100);

            if (Request.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                int targetDistance = db.Users.Where(u => u.Id == id).Select(u => u.TargetDistance).FirstOrDefault();

                // get the user's info
                var activities = (from a in db.Activities
                                  where a.ApplicationUser_Id == id
                                  select a)
                                 .OrderByDescending(a => a.ActivityDateTime);

                allActivities.Activities = activities == null ? new List<Activity>() : activities.ToList();

                allActivities.TotalDistance = allActivities.Activities.Sum(a => a.Distance);
                allActivities.PercentComplete = Math.Floor((allActivities.TotalDistance / targetDistance) * 100);

                if (allActivities.PercentComplete > 100)
                {
                    allActivities.PercentComplete = 100;
                }
            }

            return View(allActivities);
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                
            }

            ViewBag.Message = "Your application description page.";
            ViewBag.TeamName = Settings.Default.team_name;

            return View();
        }

        public ActionResult Contact()
        {
            var data = new ContactModel();
            data.TeamName = Settings.Default.team_name;
            data.TeamFundraisingLink = Settings.Default.team_fundraising_link;
            data.TeamContactEmail = Settings.Default.team_contact_email;
            data.TeamTeamOrphansLink = Settings.Default.team_team_orphans_link;

            ViewBag.Message = "Your contact page.";
            ViewBag.TeamName = Settings.Default.team_name;

            return View(data);
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