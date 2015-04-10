using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace virtualtri.Models
{
    public class HomePageModel : MyActivitiesViewModel
    {
        // base has user-specific stuff

        // need additional list of summarized stuff
        public List<UserSummary> SummaryData { get; set; }

        public float TeamTotalDistance { get; set; }

        public double TeamPercentComplete { get; set; }

        public string TeamName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string TargetEventName { get; set; }

        public string TargetEventDescription { get; set; }

        public string TargetEventLink { get; set; }
    }

    public class ContactModel
    {
        public string TeamName { get; set; }
        public string TeamContactEmail { get; set; }
        public string TeamFundraisingLink { get; set; }
        public string TeamTeamOrphansLink { get; set; }

    }

    public class UserSummary
    {
        public string DisplayName { get; set; }

        public string ApplicationUser_Id { get; set; }

        public float TotalDistance { get; set; }

        public double PercentComplete { get; set; }

        public int TargetDistance { get; set; }
    }
}