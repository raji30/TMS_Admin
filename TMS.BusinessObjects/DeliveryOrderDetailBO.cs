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
        public string Chassis { get; set; }
        public string SealNo { get; set; }
        public string Weight { get; set; }
        public DateTime AppDateFrom { get; set; }
        public DateTime AppDateTo { get; set; }
        public short Status { get; set; }
        public DateTime StatusDate { get; set; }
        public short HoldReason { get; set; }
        public DateTime HoldDate { get; set; }
        public string Comment { get; set; }
    }
}
