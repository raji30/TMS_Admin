using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class RateSheetBO
    {
        public Guid ratekey { get; set; }
        public Guid customerkey { get; set; }
        public string customername { get; set; }
        public List<ItemBO> item { get; set; }
        public Guid itemkey { get; set; }      
        public string description { get; set; }     
        public decimal unitprice { get; set; }      
        public DateTime createdate { get; set; }
        public DateTime lastupdatedate { get; set; }
        public Guid userkey { get; set; }
    }
}
