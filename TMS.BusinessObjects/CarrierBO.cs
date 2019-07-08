using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class CarrierBO
    {
        public Guid CarrierKey { get; set; }
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public bool isSteamLine { get; set; }
        public Guid AddrKey { get; set; }
        public AddressBO Address { get; set; }
        public string ScacCode { get; set; }
        public string LicensePlate { get; set; }
        public DateTime LicensePlateExpiryDate { get; set; }
        public Int16 Status { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
