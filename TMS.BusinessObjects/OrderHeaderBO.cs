using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class OrderHeaderBO
    {
        public Guid OrderKey { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerBO Customer { get; set; }
        public AddressBO BillToAddress { get; set; }
        public AddressBO SourceAddress { get; set; }
        public AddressBO DestinationAddress { get; set; }
        public AddressBO ReturnAddress { get; set; }
        public Int16 Source { get; set; }
        public Int16 OrderType { get; set; }
        public Int16 Status { get; set; }
        public DateTime StatusDate { get; set; }
        public Int16 HoldReason { get; set; }
        public DateTime HoldDate { get; set; }
        public BrokerBO Broker { get; set; }
        public string BrokerRefNo { get; set; }
        public CarrierBO Carrier { get; set; }
        public string VesselName { get; set; }
        public string BookingNo { get; set; }
        public Boolean isHazardous { get; set; }
        public Int16 Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
