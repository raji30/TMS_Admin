using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class ItemBO
    {
        public System.Guid itemkey { get; set; }
        public string itemid { get; set; }
        public string description { get; set; }
        public short itemtype { get; set; }
        public decimal unitprice { get; set; }
        public decimal unitcost { get; set; }
    }
}
