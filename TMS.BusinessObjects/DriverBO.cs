using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{

   public class DriverBO
    {
        public Guid DriverKey { get; set; }
        public string DriverId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressBO Address { get; set; }
        public Guid CarrierKey { get; set; }
        public string DriversLicenseNo { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public DateTime StatusDate { get; set; }
        public Guid VendorKey { get; set; }

    }
}
