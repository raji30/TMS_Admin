using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{/// <summary>
/// Not yet implemented
/// </summary>
    public class InvoiceController : ApiController
    {
        //
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrderstoGenerateInvoice")]
        [SwaggerOperation("GetOrderstoGenerateInvoice")]
        public HttpResponseMessage GetOrderstoGenerateInvoice()
        {
            var invoiceDtlList = dl.GetOrderstoGenerateInvoice();

            var orders = new List<InvoiceBO>();

            foreach(var list in invoiceDtlList)
            {
                InvoiceBO bo = new InvoiceBO();
                bo.order = new DeliveryOrderBO();
                bo.order.OrderDetails = new DeliveryOrderDetailBO();

                bo.order.OrderKey = list.OrderKey;
                bo.order.OrderNo = list.OrderNo;
                bo.order.OrderDate = list.OrderDate;
                bo.order.OrderType = list.OrderType;
                bo.order.BrokerRefNo = list.BrokerRefNo;
                bo.order.CustKey = list.CustKey;
                bo.order.BillToAddress = list.BillToAddress;
                bo.order.SourceAddress = list.SourceAddress;
                bo.order.DestinationAddress = list.DestinationAddress;
                bo.order.VesselName = list.VesselName;
                bo.order.BillofLading = list.BillofLading;
                bo.order.BookingNo = list.BookingNo;
                bo.order.CutOffDate = list.CutOffDate;
                bo.order.OrderDetails.OrderDetailKey = list.OrderDetails.OrderDetailKey;
                bo.order.OrderDetails.ContainerNo = list.OrderDetails.ContainerNo;
                bo.order.OrderDetails.ContainerSizeDesc = list.OrderDetails.ContainerSizeDesc;
                bo.order.OrderDetails.ContainerSize = list.OrderDetails.ContainerSize;
                bo.order.OrderDetails.Chassis = list.OrderDetails.Chassis;
                bo.order.OrderDetails.SealNo = list.OrderDetails.SealNo;

                if (list.CustKey != null)
                {
                    var addr = new AddressRepository().GetbyId(list.CustKey);
                    bo.BillFrom = new AddressBO()
                    {
                        AddrKey = addr.addrkey,
                        Address1 = addr.address1,
                        Address2 = addr.address2,
                        City = addr.city,
                        State = addr.state,
                        Zip = addr.zipcode,
                        Email = addr.email,
                        Phone = addr.phone,
                        Fax = addr.fax,
                        Name = addr.addrname

                    };
                }
                if (list.CustKey != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.BillToAddress);
                    bo.BillTo = new AddressBO()
                    {
                        AddrKey = addrBillTo.addrkey,
                        Address1 = addrBillTo.address1,
                        Address2 = addrBillTo.address2,
                        City = addrBillTo.city,
                        State = addrBillTo.state,
                        Zip = addrBillTo.zipcode,
                        Email = addrBillTo.email,
                        Phone = addrBillTo.phone,
                        Fax = addrBillTo.fax,
                        Name = addrBillTo.addrname

                    };
                }
                if (list.SourceAddress != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.SourceAddress);
                    bo.Pickup = new AddressBO()
                    {
                        AddrKey = addrBillTo.addrkey,
                        Address1 = addrBillTo.address1,
                        Address2 = addrBillTo.address2,
                        City = addrBillTo.city,
                        State = addrBillTo.state,
                        Zip = addrBillTo.zipcode,
                        Email = addrBillTo.email,
                        Phone = addrBillTo.phone,
                        Fax = addrBillTo.fax,
                        Name = addrBillTo.addrname

                    };
                }
                if (list.DestinationAddress != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.DestinationAddress);
                    bo.Delivery = new AddressBO()
                    {
                        AddrKey = addrBillTo.addrkey,
                        Address1 = addrBillTo.address1,
                        Address2 = addrBillTo.address2,
                        City = addrBillTo.city,
                        State = addrBillTo.state,
                        Zip = addrBillTo.zipcode,
                        Email = addrBillTo.email,
                        Phone = addrBillTo.phone,
                        Fax = addrBillTo.fax,
                        Name = addrBillTo.addrname

                    };
                }
                orders.Add(bo);

            }           

            return Request.CreateResponse(HttpStatusCode.OK, orders, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInvoiceMaxcount")]
        [SwaggerOperation("GetInvoiceMaxcount")]
        public HttpResponseMessage GetInvoiceMaxcount()
        {
            Int64 result = dl.GetInvoiceMaxcount();
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }
    }
}
