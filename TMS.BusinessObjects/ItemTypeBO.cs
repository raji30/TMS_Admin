using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class ItemTypeBO
    {
        public Guid itemtypekey { get; set; }
        public Int32 itemtypeid { get; set; }
        public string description { get; set; }
        public Nullable <DateTime> createdate { get; set; }
        public Nullable<Guid> createuserkey { get; set; }
        public Nullable<DateTime> lastupdatedate { get; set; }
        public Nullable<Guid> lastupdateuserkey { get; set; }
    }
}
