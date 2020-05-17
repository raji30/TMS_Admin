using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS.BusinessObjects;

namespace TMS.Api.Models
{
    public class InvoiceViewModel
    {
        public DeliveryOrderBO Order { get; set; }
        public InvoiceHeaderBO InvoiceHeader { get; set; }
        public List<InvoiceDetailBO> InvoiceDetail { get; set; }
        public IList<ThinOrderDetailViewModel> OrderDtl { get; set; }

    }
    public class ThinOrderDetailViewModel
    {
        public Guid OrderDetailKey { get; set; }
        public string ContainerNo { get; set; }
        public string Chassis { get; set; }
        public InvoiceHeaderBO InvoiceHeader { get; set; }
        public List<InvoiceDetailBO> InvoiceDetail { get; set; }
    }
}