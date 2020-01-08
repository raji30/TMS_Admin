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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Reflection;
using System.IO;
using System.Web;
using System.Net.Http.Headers;

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
                bo.orderDetails = new DeliveryOrderDetailBO();

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

                bo.orderDetails.OrderDetailKey = list.OrderDetails.OrderDetailKey;
                bo.orderDetails.ContainerNo = list.OrderDetails.ContainerNo;
                bo.orderDetails.ContainerSizeDesc = list.OrderDetails.ContainerSizeDesc;
                bo.orderDetails.ContainerSize = list.OrderDetails.ContainerSize;
                bo.orderDetails.Chassis = list.OrderDetails.Chassis;
                bo.orderDetails.SealNo = list.OrderDetails.SealNo;

                if (list.CustKey != null)
                {
                    var addr = new AddressRepository().GetbyId(list.CustKey);
                    if (addr != null)
                    {
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
                }
                if (list.CustKey != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.BillToAddress);
                    if (addrBillTo != null)
                    {
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
                }
                if (list.SourceAddress != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.SourceAddress);
                    if (addrBillTo != null)
                    {
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
                }
                if (list.DestinationAddress != null)
                {
                    var addrBillTo = new AddressRepository().GetbyId(list.DestinationAddress);
                    if (addrBillTo != null)
                    {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInvoiceTotals")]
        [SwaggerOperation("GetInvoiceTotals")]
        public HttpResponseMessage GetInvoiceTotals(string orderkey)
        {
            var invoicetotals = dl.AutoPullInvoiceCosts(orderkey);
            return Request.CreateResponse(HttpStatusCode.OK, invoicetotals, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custname"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadInvoice")]
        [SwaggerOperation("DownloadInvoice")]
        public HttpResponseMessage DownloadInvoice(string orderkey)
        {
            var invoicetotals = dl.AutoPullInvoiceCosts(orderkey);
            var fileuploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(Path.Combine(fileuploadPath, $"invoice_{orderkey}.pdf")));
            Document doc = new Document(pdfDoc);
            // By default column width is calculated automatically for the best fit.
            // useAllAvailableWidth() method set table to use the whole page's width while placing the content.
            Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            table.AddHeaderCell(new Cell().Add(new Paragraph("ContainerNo")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("ItemId")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("UnitPrice")));
            foreach (var record in invoicetotals)
            {
                table.AddCell(new Cell().Add(new Paragraph(record.ContainerNo)));
                table.AddCell(new Cell().Add(new Paragraph(record.ItemId)));
                table.AddCell(new Cell().Add(new Paragraph(record.UnitPrice.ToString())));
            }
            table.AddCell(new Cell().Add(new Paragraph()));
            table.AddCell(new Cell().Add(new Paragraph("Totals:")));
            table.AddCell(new Cell().Add(new Paragraph(invoicetotals.Select(x => x.UnitPrice).Sum().ToString())));
            doc.Add(table);
            doc.Close();
            if (doc != null) { 
            byte[] bytes = File.ReadAllBytes(Path.Combine(fileuploadPath, $"invoice_{orderkey}.pdf"));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = $"invoice_{orderkey}.pdf";

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping($"invoice_{orderkey}.pdf"));
            return response;
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty, Configuration.Formatters.JsonFormatter);
        }
    }
}
