using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace virtualtri.Models
{
    public class ParticpantsModel
    {
        public List<ParticpantModel> Participants { get; set; }

    }

    public class ParticpantModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}