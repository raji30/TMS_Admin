using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class UserDetailsBO
    {
        public Guid UserKey { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<string> UserActivity { get; set; }
        public Guid? CompanyKey { get; set; }
        public AddressBO address { get; set; }
    }
}
