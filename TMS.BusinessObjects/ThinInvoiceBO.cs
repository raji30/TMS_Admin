using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class ThinInvoiceBO
    {
        public string ItemId { get; set; }
        public Guid ItemKey { get; set; }
        public double UnitPrice { get; set; }
        public string ContainerNo { get; set; }
    }
}
