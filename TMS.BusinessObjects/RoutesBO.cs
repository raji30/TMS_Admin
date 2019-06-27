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
        public short legno { get; set; }
        public short legtype { get; set; }
        public Guid sourceaddrkey { get; set; }
        public Guid destinationaddrkey { get; set; }
        public string estimateddistanceinmiles { get; set; }
        public string estimatedtraveltime { get; set; }
        public string status { get; set; }
        public Guid driverkey { get; set; }
        public string scheduledarrival { get; set; }
        public string scheduleddeparture { get; set; }
        public string odometeratsource { get; set; }
        public short actualarrival { get; set; }
        public string actualdeparture { get; set; }
        public string odometeratdestination { get; set; }
        
    }
}
