using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class ThinOrderDO
    {
        public Guid OrderKey { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
