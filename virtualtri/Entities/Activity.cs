using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtualtri.Models;

namespace virtualtri.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        // foreign key to Participant
        public string ApplicationUser_Id { get; set; }

        // foreign key to ActivityType
        [Display(Name = "Activity Type")]
        public virtual ActivityType ActivityType { get; set; }

        public float Distance { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime ActivityDateTime { get; set; }
    }
}
