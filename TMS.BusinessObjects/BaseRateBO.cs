using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class BaseRateBO
    {
        public Guid baseratekey { get; set; }
        public Guid customerkey { get; set; }
        public string customername { get; set; }
        public Guid citykey { get; set; }
        public string cityname { get; set; }
        public string description { get; set; }
        public decimal unitprice { get; set; }
        public DateTime createdate { get; set; }
        public DateTime lastupdatedate { get; set; }
        public Guid userkey { get; set; }
    }
}
