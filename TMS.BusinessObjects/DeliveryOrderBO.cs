using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class DeliveryOrderBO
    {
        public Guid OrderKey { get; set; }
        public string OrderNo { get; set; }
        public Guid CustKey { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid BillToAddress { get; set; }
        public Guid SourceAddress { get; set; }
        public Guid DestinationAddress { get; set; }
        public Guid ReturnAddress { get; set; }
        public short Source { get; set; }
        public short OrderType { get; set; }
        public short Status { get; set; }
        public DateTime StatusDate { get; set; }
        public short HoldReason { get; set; }
        public string HoldDate { get; set; }
        public string BrokerName { get; set; }
        public string BrokerId { get; set; }
        public Guid Brokerkey { get; set; }
        public string BrokerRefNo { get; set; }
        public Guid PortofOriginKey { get; set; }
        public Guid PortofDestinationKey { get; set; }
        public Guid CarrierKey { get; set; }
        public string VesselName { get; set; }
        public string BillofLading { get; set; }
        public string BookingNo { get; set; }
        public DateTime CutOffDate { get; set; }
        public short Priority { get; set; }
        public bool IsHazardous { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ordertypedescription{ get; set; }
        public string statusdescription{ get; set; }
      public DeliveryOrderDetailBO OrderDetails { get; set; }
    }
}
