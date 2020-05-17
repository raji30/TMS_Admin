using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class SchedulerBO
    {       
        public Guid OrderDetailKey { get; set; }
        public Guid RouteKey { get; set; }
        public DateTime? AppDateFrom { get; set; }
        public DateTime? AppDateTo { get; set; }
        public string SchedulerNotes { get; set; }
        public DateTime? LastFreeDay { get; set; }
        public short Status { get; set; }       
        public short LegType { get; set; } 
        public string DriverNotes { get; set; }
        public DateTime? ScheduleArrival { get; set; }
        public DateTime? ScheduleDeparture { get; set; }

        public List<AccountingBO> accountingBO { get; set; }


    }
}
