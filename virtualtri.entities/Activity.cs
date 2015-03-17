using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtualtri.entities
{
    public class Activity
    {
        public int Id { get; set; }

        // foreign key to Participant
        public virtual ApplicationUser ApplicationUser { get; set; }

        // foreign key to ActivityType
        public virtual ActivityType ActivityType { get; set; }

        public float Distance { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime ActivityDateTime { get; set; }
    }
}
