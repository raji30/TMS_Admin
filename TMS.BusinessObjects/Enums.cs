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
            SendtoScheduler = 3,
            SendtoDispatchAssignment = 4,
            SendtoDispatchDelivery = 5,          
            SendtoBilling = 6,
            InvoiceGenerated = 7,
            Reviewed = 8,
            Complete = 9,
            OnHold = 10,
        }
       public enum AddressType
        {
            Customer = 1,
            Company = 2,
            Vendor = 3,
            ShippingPort = 4,
            Terminal = 5,
            Driver = 6,
            Warehouse = 7
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
        public enum Source
        {
            Source1 = 1,
            Source2 = 2,
            Source3 = 3
        }

        public enum Carrier
        {
            MAEU = 1,
            SAFM = 2,
            SEGO = 3
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
