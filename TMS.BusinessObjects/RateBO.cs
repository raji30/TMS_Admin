using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class RateBO
    {
        public string containerno { get; set; }
        public Guid itemkey { get; set; }
        public string itemid { get; set; }
        public string description { get; set; }        
        public decimal unitprice { get; set; }     
        public decimal baserate { get; set; }
    }
}
