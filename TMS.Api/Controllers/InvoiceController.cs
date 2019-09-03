using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{/// <summary>
/// Not yet implemented
/// </summary>
    public class InvoiceController : ApiController
    {
        InvoiceDL dl = new InvoiceDL();
        [HttpGet]
        [Route("Get/{InvoiceNo}")]
        [SwaggerOperation("Get")]
        public HttpResponseMessage Get(string InvoiceNo)
        {
            InvoiceHeaderBO invoice = dl.GetInvoice(InvoiceNo);
            return Request.CreateResponse(HttpStatusCode.OK, invoice, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        [Route("CreateInvoiceHeader")]
        [SwaggerOperation("CreateInvoiceHeader")]
        public HttpResponseMessage Post([FromBody]InvoiceHeaderBO bo )
        {
            var invoiceHeader =dl.PostInvoice(bo);
            return Request.CreateResponse(HttpStatusCode.OK, invoiceHeader, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetInvoicebyOrderDetailKey")]
        [SwaggerOperation("GetInvoicebyOrderDetailKey")]
        public HttpResponseMessage GetbyOrderDetailKey([FromBody]string orderdetailkey)
        {
            var invoiceHeader = dl.GetInvoicebyOrderDetailKey(orderdetailkey);
            return Request.CreateResponse(HttpStatusCode.OK, invoiceHeader, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetInvoiceDetail")]
        [SwaggerOperation("GetInvoiceDetail")]
        public HttpResponseMessage GetInvoiceDetails ([FromBody]string OrderDetailKey)
        {
            var invoiceDtlList = dl.GetInvoiceDetail(OrderDetailKey);
            return Request.CreateResponse(HttpStatusCode.OK, invoiceDtlList, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("CreateInvoiceDetail")]
        [SwaggerOperation("CreateInvoiceDetail")]
        public HttpResponseMessage CreateInvoiceDetail([FromBody]InvoiceDetailBO invoiceDetail)
        {
            var invoiceDtl = dl.PostInvoiceDetail(invoiceDetail);
            return Request.CreateResponse(HttpStatusCode.OK, invoiceDtl, Configuration.Formatters.JsonFormatter);
        }
    }
}
