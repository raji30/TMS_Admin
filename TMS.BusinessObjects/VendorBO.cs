using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class VendorBO
    {
        public System.Guid vendkey { get; set; }
        public string vendid { get; set; }
        public string vendname { get; set; }
        public AddressBO Address { get; set; }
        public Guid addrkey { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<System.DateTime> statusdate { get; set; }
    }
}
