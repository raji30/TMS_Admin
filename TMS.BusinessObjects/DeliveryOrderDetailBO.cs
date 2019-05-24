using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class DeliveryOrderDetailBO
    {
        public Guid OrderDetailKey { get; set; }
        public Guid OrderKey { get; set; }
        public string ContainerNo { get; set; }
        public short ContainerSize { get; set; }
        public string ContainerSizeDesc { get; set; }
        public string Chassis { get; set; }
        public string SealNo { get; set; }
        public string Weight { get; set; }
        public string AppDateFrom { get; set; }
        public string AppDateTo { get; set; }
        public short Status { get; set; }
        public string StatusDesc { get; set; }
        public string StatusDate { get; set; }
        public short HoldReason { get; set; }
        public string HoldReasonDesc { get; set; }
        public string HoldDate { get; set; }
        public string Comment { get; set; }
    }
}
