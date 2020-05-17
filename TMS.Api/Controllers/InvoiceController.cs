using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using TMS.Api.Models;
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
using TMS.BusinessLayer;

namespace TMS.Api.Controllers
{
    [AllowAnonymous]
    public class InvoiceController : ApiController
    {
        //
        InvoiceBL bl = new InvoiceBL();
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
        public HttpResponseMessage CreateInvoiceDetail([FromBody]InvoiceDetailBO[] invoiceDetail)
        {
            var invoiceDtl = dl.PostInvoiceDetail(invoiceDetail);
            return Request.CreateResponse(HttpStatusCode.OK, invoiceDtl, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceDetail"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateInvoiceDetail")]
        [SwaggerOperation("UpdateInvoiceDetail")]
        public HttpResponseMessage UpdateInvoiceDetail([FromBody]InvoiceDetailBO[] invoiceDetail)
        {

            foreach(var inv in invoiceDetail)
            {
                if(inv.InvoiceLineKey==null)
                {
                    var invoiceDtl = dl.PostInvoiceDetail(inv);
                }
                else
                {
                    var invoiceDtl = dl.UpdateInvoiceDetail(inv);
                }
            }
          
            return Request.CreateResponse(HttpStatusCode.OK, true, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateInvoiceHeader")]
        [SwaggerOperation("UpdateInvoiceHeader")]
        public HttpResponseMessage UpdateInvoiceHeader([FromBody]InvoiceHeaderBO InvoiceHeader)
        {
            var invoiceDtl = dl.UpdateInvoice(InvoiceHeader);            

            return Request.CreateResponse(HttpStatusCode.OK, true, Configuration.Formatters.JsonFormatter);
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
               // bo.order.CutOffDate = list.CutOffDate;

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



        [HttpGet]
        [Route("getOrderDatabyKey/{orderkey}")]
        [SwaggerOperation("getOrderDatabyKey")]
        public HttpResponseMessage getOrderDatabyKey(string orderkey)
        {
            InvoiceBO invoiceBO = new InvoiceBO();
            var orderdata = bl.getOrderDatabyKey(orderkey);
                invoiceBO.order = orderdata;

           var containerdata = dl.GetContainers(orderkey);

            if (containerdata != null)
            {
                invoiceBO.containers = containerdata;
            }

            if (invoiceBO != null)
                return Request.CreateResponse(HttpStatusCode.OK, invoiceBO, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("getorderratesbykey/{orderkey}")]
        [SwaggerOperation("getorderratesbykey")]
        public HttpResponseMessage getorderratesbykey(string orderkey)
        {
            var rates = bl.getorderratesbykey(orderkey);

            if (rates != null)
                return Request.CreateResponse(HttpStatusCode.OK, rates, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }


        [HttpGet]
        [Route("GetInvoiceHeaderList")]
        [SwaggerOperation("GetInvoiceHeaderList")]
        public HttpResponseMessage GetInvoiceHeaderList()
        {           
            List<InvoiceHeaderBO> dorder = dl.GetInvoiceHeaderList();
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet]
        [Route("getinvoicedetail/{invoicekey}")]
        [SwaggerOperation("getinvoicedetail")]
        public HttpResponseMessage getinvoicedetail(string invoicekey)
        {
            List<InvoiceDetailBO> dorder = dl.getinvoicedetail(invoicekey);
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
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
        [HttpGet]
        [Route("CreateInvoicePDF/{orderkey}")]
        [SwaggerOperation("CreateInvoicePDF")]
        public HttpResponseMessage CreateInvoicePDF(string orderkey)
        {
           
            var viewmodel = new InvoiceViewModel();
         //   string orderKey = "33215b82-91a6-11e9-b6c9-3f0945f4a3b5";
            DeliveryOrderDL orderDl = new DeliveryOrderDL();
            InvoiceDL dl = new InvoiceDL();
            var orderBO = orderDl.GetDeliveryOrder(orderkey);
            viewmodel.Order = orderBO;
            var orderdetailBO = orderDl.GetOrderDetailsByKey(orderkey);
            if (orderdetailBO != null && orderdetailBO.Count > 0)
            {
                var orderdtlList = new List<ThinOrderDetailViewModel>();
                orderdetailBO.ForEach(d =>
                {
                    orderdtlList.Add(new ThinOrderDetailViewModel()
                    {
                        Chassis = d.Chassis,
                        ContainerNo = d.ContainerNo,
                        OrderDetailKey = d.OrderDetailKey,
                        InvoiceHeader = d.OrderDetailKey != null ? dl.GetInvoicebyOrderDetailKey(Convert.ToString(d.OrderDetailKey)) : null,
                        InvoiceDetail = d.OrderDetailKey != null ? dl.GetInvoiceDetail(Convert.ToString(d.OrderDetailKey)) : null,

                    });
                });
                viewmodel.OrderDtl = orderdtlList;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Order not found", Configuration.Formatters.JsonFormatter);
            }
            var stringified = new InvoiceViewController().RenderRazorViewToString("~/Views/InvoiceView/Index.cshtml", viewmodel);
            var pdf = IronPdf.HtmlToPdf.StaticRenderHtmlAsPdf(stringified);
            var basepath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
           var doccreated= pdf.SaveAs(Path.Combine(basepath, $"{viewmodel.Order.OrderNo}.pdf"));
            if(doccreated.PageCount > 0)
            return Request.CreateResponse(HttpStatusCode.OK,"Invoice generated", Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occurred while creating Invoice", Configuration.Formatters.JsonFormatter);
           
        }

        [HttpGet]
        [Route("DownloadInvoice/{orderno}")]
        [SwaggerOperation("DownloadInvoice")]
        public HttpResponseMessage DownloadInvoice(string orderno)
        {
           
            var fileuploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            var filename = Path.Combine(fileuploadPath, $"{orderno}.pdf");
            if(File.Exists(filename)) { 
           
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(new FileStream(filename, FileMode.Open, FileAccess.Read));
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = filename ;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "File Not found", Configuration.Formatters.JsonFormatter);
        }


        [HttpGet]
        [Route("CreatePDFforInvoice/{invoicekey}")]
        [SwaggerOperation("CreateInvoicePDF")]
        public HttpResponseMessage CreatePDFforInvoice(string invoicekey)
        {
            var viewmodel = new InvoiceViewModel();   
            DeliveryOrderDL orderDl = new DeliveryOrderDL();
            InvoiceDL dl = new InvoiceDL();

            string orderkey = dl.getOrderkeybyInvoicekey(invoicekey);

            if(!string.IsNullOrEmpty(orderkey))
            {
                var orderBO = orderDl.GetDeliveryOrder(orderkey);
                viewmodel.Order = orderBO;
                viewmodel.InvoiceHeader = dl.GetInvoicebyinvoiceKey(invoicekey);
                viewmodel.InvoiceDetail = dl.GetInvoiceDetail(invoicekey);

                var orderdetailBO = orderDl.GetOrderDetailsByKey(orderkey);

                if (orderdetailBO != null && orderdetailBO.Count > 0)
                {
                    var orderdtlList = new List<ThinOrderDetailViewModel>();
                    orderdetailBO.ForEach(d =>
                    {
                        orderdtlList.Add(new ThinOrderDetailViewModel()
                        {
                            Chassis = d.Chassis,
                            ContainerNo = d.ContainerNo,
                            OrderDetailKey = d.OrderDetailKey,
                            //InvoiceHeader = d.OrderDetailKey != null ? dl.GetInvoicebyinvoiceKey(Convert.ToString(invoicekey)) : null,
                            //InvoiceDetail = d.OrderDetailKey != null ? dl.GetInvoiceDetail(invoicekey) : null,

                        });
                    });
                    viewmodel.OrderDtl = orderdtlList;
                }
            }            
            //if (orderdetailBO != null && orderdetailBO.Count > 0)
            //{
            //    var orderdtlList = new List<ThinOrderDetailViewModel>();
            //    orderdetailBO.ForEach(d =>
            //    {
            //        orderdtlList.Add(new ThinOrderDetailViewModel()
            //        {
            //            Chassis = d.Chassis,
            //            ContainerNo = d.ContainerNo,
            //            OrderDetailKey = d.OrderDetailKey,
            //            InvoiceHeader = d.OrderDetailKey != null ? dl.GetInvoicebyOrderDetailKey(Convert.ToString(d.OrderDetailKey)) : null,
            //            InvoiceDetail = d.OrderDetailKey != null ? dl.GetInvoiceDetail(Convert.ToString(d.OrderDetailKey)) : null,

            //        });
            //    });
            //    viewmodel.OrderDtl = orderdtlList;
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Order not found", Configuration.Formatters.JsonFormatter);
            //}

            if(viewmodel==null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Order not found", Configuration.Formatters.JsonFormatter);
            }

            var stringified = new InvoiceViewController().RenderRazorViewToString("~/Views/InvoiceView/Invoice.cshtml", viewmodel);
            var pdf = IronPdf.HtmlToPdf.StaticRenderHtmlAsPdf(stringified);
            var basepath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            var doccreated = pdf.SaveAs(Path.Combine(basepath, $"{viewmodel.Order.OrderNo}.pdf"));
            if (doccreated.PageCount > 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Invoice generated", Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occurred while creating Invoice", Configuration.Formatters.JsonFormatter);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="custname"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("DownloadInvoice_1")]
        //[SwaggerOperation("DownloadInvoice_1")]
        private HttpResponseMessage DownloadInvoice_1(string orderkey)
        {
            // var invoicetotals = dl.AutoPullInvoiceCosts(orderkey);
            var fileuploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Files/");
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(Path.Combine(fileuploadPath, $"invoice_{orderkey}.pdf")));
            DeliveryOrderDL doObj = new DeliveryOrderDL();
            var orderHeader = doObj.GetDeliveryOrder(orderkey);
            var customer = new CustomerDL().GetCustomerbykey(orderHeader.CustKey);

            var orderdetaillist = doObj.GetOrderDetailsByKey(orderkey);
            Document doc = new Document(pdfDoc);
            /*
             1483 Via Plata StreetLong Beach, CA  
             */
            var paragraph = new Paragraph("INVOICE").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
            doc.Add(paragraph);
           
            var cell = new Cell().SetTextAlignment(TextAlignment.LEFT);
            cell.Add(new Paragraph(new Text("PLEASE REMIT TO").SetFontSize(14).SetBold()));
            cell.Add(new Paragraph("Junction Collaborative Transports").SetFontSize(12));
            cell.Add(new Paragraph("1483 Via Plata Street").SetFontSize(12));
            cell.Add(new Paragraph("Long Beach, CA").SetFontSize(12));
            cell.Add(new Paragraph("90810").SetFontSize(12));
            cell.Add(new Paragraph("Phone: (310) 537-7730").SetFontSize(12));
            cell.Add(new Paragraph("Fax: (310) 537-7723").SetFontSize(12));
           
            //p.Add(new Paragraph().Add("PLEASE REMIT TO").Add(customer.CustName).Add(customer.Address.Address1)
            //    .Add(customer.Address.Address2).Add(customer.Address.City).Add(customer.Address.State)
            //    .Add(customer.Address.Zip));
            doc.Add(cell);
            
            List header = new List();
            double runningtotal = 0.0;
            InvoiceDL dl = new InvoiceDL();
            foreach (var orderdetail in orderdetaillist)
            {
                var invoice = dl.GetInvoicebyOrderDetailKey(orderdetail.OrderDetailKey.ToString());
                Table dtlTable = new Table(UnitValue.CreatePercentArray(7)).UseAllAvailableWidth();
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Container no")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Container size")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Chassis")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Invoice No")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Due date")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Invoice Date")));
                dtlTable.AddHeaderCell(new Cell().Add(new Paragraph("Amount")));
                dtlTable.AddCell(new Cell().Add(new Paragraph(orderdetail.ContainerNo)));
                dtlTable.AddCell(new Cell().Add(new Paragraph(orderdetail.ContainerSize.ToString())));
                dtlTable.AddCell(new Cell().Add(new Paragraph(orderdetail.Chassis)));
                dtlTable.AddCell(new Cell().Add(new Paragraph(invoice.InvoiceNo.ToString())));
                dtlTable.AddCell(new Cell().Add(new Paragraph(invoice.DueDate.ToString())));
                dtlTable.AddCell(new Cell().Add(new Paragraph(invoice.InvoiceDate.ToString())));
                dtlTable.AddCell(new Cell().Add(new Paragraph(invoice.InvoiceAmt.ToString())));
                runningtotal += (double)invoice.InvoiceAmt;
                ListItem dtl = new ListItem();
                dtl.Add(new Paragraph().Add(dtlTable));
              
               // doc.Add(dtlTable);
                
               var invoicedtl=  dl.GetInvoiceDetail(orderdetail.OrderDetailKey.ToString());
                ListItem invoiceItem = new ListItem();
                Table table = new Table(UnitValue.CreatePercentArray(5));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Item")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Price")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Quantity")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("UnitPrice")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Excess Amount")));
              
                foreach(var invoiceline in invoicedtl)
                {
                    table.AddCell(new Cell().Add(new Paragraph(invoiceline.Description)));
                    table.AddCell(new Cell().Add(new Paragraph(invoiceline.Price.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(invoiceline.Quantity.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(invoiceline.UnitPrice.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(invoiceline.ExcessAmount.ToString())));
                    runningtotal += (double)invoiceline.Price * (double) invoiceline.Quantity;
                }

                invoiceItem.Add(new Paragraph("Detailed Breakup:").SetFirstLineIndent(25).Add(table));
                dtl.Add(invoiceItem);
                header.Add(dtl);
            }
            header.SetSymbolIndent(80).Add($"Total:{runningtotal.ToString()}");
            doc.Add(header);
            //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           
            
            // By default column width is calculated automatically for the best fit.
            // useAllAvailableWidth() method set table to use the whole page's width while placing the content.
           
            //foreach (var record in invoicetotals)
            //{
            //    table.AddCell(new Cell().Add(new Paragraph(record.ContainerNo)));
            //    table.AddCell(new Cell().Add(new Paragraph(record.ItemId)));
            //    table.AddCell(new Cell().Add(new Paragraph(record.UnitPrice.ToString())));
            //}
            //table.AddCell(new Cell().Add(new Paragraph()));
            //table.AddCell(new Cell().Add(new Paragraph("Totals:")));
            //table.AddCell(new Cell().Add(new Paragraph(invoicetotals.Select(x => x.UnitPrice).Sum().ToString())));
            //doc.Add(table);
            //}
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
