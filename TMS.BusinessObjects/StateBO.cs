using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class StateBO
    {
        public Guid statekey { get; set; }
        public string stateid { get; set; }
        public string statename { get; set; }
        public DateTime createdate { get; set; }
        public Int16 Status { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
