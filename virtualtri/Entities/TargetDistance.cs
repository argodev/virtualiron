using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace virtualtri.Entities
{
    public class TargetDistance
    {
        public int Distance { get; set; }
        public string Name { get; set; }
    }

    public class Utils {
        public static IEnumerable<TargetDistance> Distances = new List<TargetDistance> { 
            new TargetDistance {
                Distance = 140,
                Name = "Regular Iron (140.6 Miles)"
            },
            new TargetDistance {
                Distance = 282,
                Name = "Double Iron (282 Miles)"
            },
            new TargetDistance {
                Distance = 500,
                Name = "Mega Iron (500 Miles)"
            }
        };
    }
}