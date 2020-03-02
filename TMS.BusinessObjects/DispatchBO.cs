using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class DispatchBO
    {       
        public Guid? Routekey { get; set; }
        public Guid? OrderDetailKey { get; set; }       
        public string appointmentno { get; set; }
        public string containerid { get; set; }
        public string containerno { get; set; }
        public Guid? driverkey { get; set; }
        public string driverid { get; set; }
        public string drivernotes { get; set; }
        public string chassis { get; set; }
        public string legno { get; set; }
        public short legtype { get; set; }
        public string legtypeDesc { get; set; }
        public DateTime? portwaitingtimefrom { get; set; }
        public DateTime? portwaitingtimeto { get; set; }
        public DateTime? customerwaitingtimefrom { get; set; }
        public DateTime? customerwaitingtimeto { get; set; }   
        public DateTime? actualarrival { get; set; }
        public DateTime? actualdeparture { get; set; }
        public short status { get; set; }
    }
}
