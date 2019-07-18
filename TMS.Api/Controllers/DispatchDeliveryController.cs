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
        public HttpResponseMessage Put([FromBody]RoutesBO routesBO)
        {
            var routesdata = routes.UpdateRouteData(routesBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }
    }
}
