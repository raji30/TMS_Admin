using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TMS.Api.Models;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{
    [AllowAnonymous]
    public class InvoiceViewController : Controller
    {
        // GET: InvoiceView
        //public ActionResult Index()
       // {
            //var viewmodel = new InvoiceViewModel();
            //string orderKey = "33215b82-91a6-11e9-b6c9-3f0945f4a3b5";
            //DeliveryOrderDL orderDl = new DeliveryOrderDL();
            //InvoiceDL dl = new InvoiceDL();
            //var orderBO = orderDl.GetDeliveryOrder(orderKey);
            //viewmodel.Order = orderBO;
            //var orderdetailBO = orderDl.GetOrderDetailsByKey(orderKey);
            //if(orderdetailBO != null && orderdetailBO.Count > 0)
            //{ 
            //var orderdtlList = new List<ThinOrderDetailViewModel>();
            //orderdetailBO.ForEach(d =>                   
            //{
            //    orderdtlList.Add(new ThinOrderDetailViewModel()
            //    {
            //        Chassis = d.Chassis,
            //        ContainerNo = d.ContainerNo,
            //        OrderDetailKey = d.OrderDetailKey,
            //        InvoiceHeader = d.OrderDetailKey != null? dl.GetInvoicebyOrderDetailKey(Convert.ToString(d.OrderDetailKey)): null,
            //        InvoiceDetail = d.OrderDetailKey != null ? dl.GetInvoiceDetail(Convert.ToString(d.OrderDetailKey)) : null,

            //    });
            //});
            //    viewmodel.OrderDtl = orderdtlList;
            //}

            //var stringified = RenderRazorViewToString("~/Views/InvoiceView/Index.cshtml", viewmodel);
            //var pdf = IronPdf.HtmlToPdf.StaticRenderHtmlAsPdf(stringified);
            //var basepath = Server.MapPath("~/App_Data/Files/");
            //var fileName = Path.Combine(basepath, $"{viewmodel.Order.OrderNo}.pdf");
            //if (System.IO.File.Exists(fileName))
            //{
            //    System.IO.File.Delete(fileName);
            //}
            //pdf.SaveAs(Path.Combine(basepath, $"{viewmodel.Order.OrderNo}.pdf"));
            //System.Diagnostics.Process.Start(Path.Combine(basepath, $"{viewmodel.Order.OrderNo}.pdf"));
            //return View(viewmodel);
        //}
        [NonAction]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                //var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                //viewName);
                //var viewContext = new ViewContext(ControllerContext, viewResult.View,
                //ViewData, TempData, sw);
                //viewResult.View.Render(viewContext, sw);
                //viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                //return sw.GetStringBuilder().ToString();

                //var routeData = new RouteData();
                //routeData.Values.Add("controller", "InvoiceView");
                //ControllerContext controllerContext = new System.Web.Mvc.ControllerContext();
                //controllerContext.RouteData = routeData;

                //var viewResult = ViewEngines.Engines.FindPartialView(controllerContext,
                //viewName);
                //var viewContext = new ViewContext(ControllerContext, viewResult.View,
                //ViewData, TempData, sw);
                //viewResult.View.Render(viewContext, sw);
                //viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                //return sw.GetStringBuilder().ToString();

                var routeData = new RouteData();
                routeData.Values.Add("controller", "IndexView");
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);
                var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(ViewData), new TempDataDictionary(), sw);
                razorViewResult.View.Render(viewContext, sw);
                return sw.ToString();
            }


        }
        public class FakeController : ControllerBase { protected override void ExecuteCore() { } }

    }
}