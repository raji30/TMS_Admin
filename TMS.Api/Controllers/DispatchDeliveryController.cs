using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{
    [AllowAnonymous]
    public class DispatchDeliveryController : ApiController
    {
        RoutesDL routes = new RoutesDL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrderstoDispatchDelivery")]
        [SwaggerOperation("GetOrderstoDispatchDelivery")]
        public HttpResponseMessage GetOrderstoDispatchDelivery()
        {
            DispatchDeliveryDL dl = new DispatchDeliveryDL();
            var list = dl.GetOrderstoDispatchDelivery();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routesBO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateDispatchDeliveryData")]
        [SwaggerOperation("UpdateDispatchDeliveryData")]
        public HttpResponseMessage Put([FromBody]DispatchBO[] routesBO)
        {
            var routesdata = routes.UpdateRouteDataforDispatchDelivery(routesBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }


        [HttpPut]
        [Route("UpdateStatus")]
        [SwaggerOperation("UpdateStatus")]
        public HttpResponseMessage UpdateStatus([FromBody]DispatchBO routesBO)
        {
            var routesdata = routes.UpdateStatus(routesBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetDispatchItemsList")]
        [SwaggerOperation("GetDispatchItemsList")]
        public HttpResponseMessage GetDispatchItemsList()
        {
            DispatchDeliveryDL dl = new DispatchDeliveryDL();

            List<DeliveryOrderBO> dorder = dl.GetDispatchItemsList();

            if(dorder != null)
            {
                foreach(var data in dorder)
                {
                   var Details  = new List<DispatchBO>();
                    Details = dl.GetDispatchItems(data.OrderDetails.OrderDetailKey);
                    data.dispatchdetails = Details;
                }                
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetDispatch_OrderandDetails/{orderdetailkey}")]
        [SwaggerOperation("GetDispatch_OrderandDetails")]
        public HttpResponseMessage GetDispatch_OrderandDetails(string orderdetailkey)
        {
            DispatchDeliveryDL dl = new DispatchDeliveryDL();

            List<DeliveryOrderBO> dorder = dl.GetDispatch_OrderandDetails(Guid.Parse(orderdetailkey));          

            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetDispatchItems/{orderdetailkey}")]
        [SwaggerOperation("GetDispatchItems")]
        public HttpResponseMessage GetDispatchItems(string orderdetailkey)
        {
            DispatchDeliveryDL dl = new DispatchDeliveryDL();
            var Details = dl.GetDispatchItems(Guid.Parse(orderdetailkey));                  
            
            return Request.CreateResponse(HttpStatusCode.OK, Details, Configuration.Formatters.JsonFormatter);
        }
    }
}
