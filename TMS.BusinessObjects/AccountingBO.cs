using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class AccountingBO
    {
        public Guid itemskey { get; set; }
        public Guid itemkey { get; set; }
        public string itemid { get; set; }
        public string description { get; set; }
        public short itemtype { get; set; }
        public Guid orderdetailkey { get; set; }
        public Guid customerkey { get; set; }
        public DateTime createdate { get; set; }
        public Guid createuserkey { get; set; }
        public DateTime lastupdatedate { get; set; }
        public Guid lastupdateuserkey { get; set; }
    }
}
