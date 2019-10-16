using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TMS.BusinessObjects.Enums;

namespace TMS.BusinessObjects
{
   public class DocumentBO
    {
        public Guid Dockey { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DocType DocType { get; set; }
        public int FileSizeInMB { get; set; }
    }

    public class OrderHeaderDocumentBO
    {
        public Guid Orderkey { get; set; }
        public string OrderNo { get; set; }
        public DocumentBO Document { get; set; }
    }
    public class OrderDetailDocumentBO
    {
        public Guid OrderDetailKey { get; set; }
        public DocumentBO Document { get; set; }
    }
}
