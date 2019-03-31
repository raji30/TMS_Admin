﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public static class Enums
    {
      public  enum DOStatus
        {
            InProgress = 1,
            CreditDenied = 2,
            SenttoScheduler = 3,
            SenttoDispatch = 4,
            OnHold =5,
            SenttoBilling = 6,
            InvoiceGenerated = 7,
            Reviewed = 8,
            Complete = 9
        }
       public enum AddressType
        {
            Customer = 1,
            Vendor = 2,
            ShippingPort = 3,
            Terminal = 4,
            Driver = 5,
            Warehouse = 6
        }
        public enum ContainerSize
        {
            Small =1,
            Medium =2,
            Large = 3

        }
        public enum OrderType
        {
            Import =1,
            ImportNoReturn =2,
            Export = 3,
            ExportNoEmptyPickup = 4,
            ReturnToTerminal = 5,
            OneWayDelivery = 6
        }
    }
}
