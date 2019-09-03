using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class InvoiceDetailBO
    {
        public Guid Itemkey { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public short ItemType { get; set; }
        public decimal Quantity { get; set; }
        public string InvoiceDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExcessAmount { get; set; }
        public Guid InvoiceLineKey { get; set; }
        public Guid InvoiceKey { get; set; }
    }
}
