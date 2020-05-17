using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class UserRoleBO
    {
        public Guid userkey { get; set; }
        public Guid rolekey { get; set; }
        public string description { get; set; }
    }
}
