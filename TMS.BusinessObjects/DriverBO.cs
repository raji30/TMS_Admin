using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class DriverBO
    {
        public Guid driverkey { get; set; }
        public Guid addrkey { get; set; }
        public Guid carrierkey { get; set; }
        public Guid vendkey { get; set; }
        public string driverid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string drivinglicenseno { get; set; }
        public DateTime drivinglicenseexpirydate { get; set; }
        public DateTime createdate { get; set; }
        public short status { get; set; }
        public DateTime statusdate { get; set; }      
    }
}
