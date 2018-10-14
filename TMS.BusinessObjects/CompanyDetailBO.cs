using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class CompanyDetailBO
    {
        public Guid CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ParentCompanyKey { get; set; }
        public AddressBO address { get; set; }
    }
}
