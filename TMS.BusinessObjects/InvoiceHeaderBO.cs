using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class InvoiceHeaderBO
    {
        public Guid Invoicekey { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Guid CustKey { get; set; }
        public Guid BilltoAddrKey { get; set; }
        public Guid BilltoAddrCopy { get; set; }
        public decimal InvoiceAmt { get; set; }
        public DateTime DueDate { get; set; }
        public int InvoiceType { get; set; }
        public Guid OrderDetailKey { get; set; }
    }
}
