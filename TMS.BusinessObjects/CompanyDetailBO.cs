using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class CompanyDetailBO
    {
        public Guid compkey { get; set; }
        public string compid { get; set; }
        public string compname { get; set; }
        public Guid? ParentCompanyKey { get; set; }
        public int status { get; set; }
        public Guid addrkey { get; set; }
        public AddressBO Address { get; set; }
    }
}
