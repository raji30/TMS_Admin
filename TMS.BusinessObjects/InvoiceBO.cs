﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class InvoiceBO
    {
        public DeliveryOrderBO order { get; set; }
        public AddressBO BillFrom { get; set; }
        public AddressBO BillTo { get; set; }
        public AddressBO Pickup { get; set; }
        public AddressBO Delivery { get; set; }
        public AddressBO Broker { get; set; }
    }


}
