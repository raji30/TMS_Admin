using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class CommentBO
    {
        public Guid commentkey { get; set; }
        public string description { get; set; }      
        public Nullable<DateTime> createdate { get; set; }
        public Nullable<Guid> createuserkey { get; set; }
    }
}
