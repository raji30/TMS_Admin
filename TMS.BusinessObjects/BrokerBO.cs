using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class BrokerBO
    {
        public Guid BrokerKey { get; set; } 
        public string BrokerId { get; set; }
        public string BrokerName { get; set; }
        public AddressBO Address { get; set; }
        public Int16 Status { get; set; }

    }
}
