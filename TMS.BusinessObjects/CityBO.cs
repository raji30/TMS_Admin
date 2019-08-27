using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class CityBO
    {
        public Guid citykey { get; set; }
        public string cityid { get; set; }
        public string cityname { get; set; }
        public DateTime createdate { get; set; }
        public Int16 Status { get; set; }      
        public DateTime StatusDate { get; set; }  
    }
}
