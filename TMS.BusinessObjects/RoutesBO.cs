using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class RoutesBO
    {
        public Guid Routekey { get; set; }
        public Guid OrderDetailKey { get; set; }
        public Guid OrderKey { get; set; }
        public int legno { get; set; }
        public int legtype { get; set; }
        public Guid sourceaddrkey { get; set; }
        public Guid destinationaddrkey { get; set; }
        public string estimateddistanceinmiles { get; set; }
        public string estimatedtraveltime { get; set; }
        public string status { get; set; }
        public Guid driverkey { get; set; }
        public DateTime scheduledarrival { get; set; }
        public DateTime scheduleddeparture { get; set; }
        public string odometeratsource { get; set; }
        public DateTime actualarrival { get; set; }
        public DateTime actualdeparture { get; set; }
        public string odometeratdestination { get; set; }
        public string drivernotes { get; set; }
    }
}
