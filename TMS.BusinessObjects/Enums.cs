using System;
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
            DRY_20 =1,
            DRY_40 =2,
            HDRY_40 = 3,
            HDRY_45 = 4,
            OPEN_20 = 5,
            OPEN_40=6,
            FLAT_20 =7,
            FLAT_40 =8
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

        public enum Priority
        {
            High = 1,
            Medium = 2,
            Low = 3
        }

        public enum HoldReason
        {           
            NoConfirmationFromCustomer = 1,
            DriverAvailability = 2,
            ContainerAvailability = 3                
        }
        public enum DocType
        {
            PDF=1,
            DOC=2,
            DOCX=3,
            XLS = 4,
            XLSX =5,
            JPG=6
        }
    }
}
