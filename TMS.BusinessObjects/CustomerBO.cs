using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class CustomerBO
    {
        public Guid CustomerKey { get; set; }
        public string CustId { get; set; }
        public string CustName { get; set; }
        public AddressBO Address { get; set; }
        public Int16 Status { get; set; }
        public Int16? CustomerGroup { get; set; }
        public DateTime StatusDate { get; set; }
        public bool CreditCheck { get; set; }
        public decimal? CreditLimit { get; set; }
        public Int16? CreditStatus { get; set; }
    }
}
