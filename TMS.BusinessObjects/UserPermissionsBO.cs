using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class UserPermissionsBO
    {
        public Guid PermissionKey { get; set; }
        public Guid UserKey { get; set; }       
        public string Modulename { get; set; }
        public Int16 fView { get; set; }
        public Int16 fNew { get; set; }
        public Int16 fEdit { get; set; }
        public Int16 fDelete { get; set; }      
        public Int16 Status { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
