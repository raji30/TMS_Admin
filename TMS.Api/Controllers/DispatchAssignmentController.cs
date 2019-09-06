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
    public class DispatchAssignmentController : ApiController
    {
        RoutesDL routes = new RoutesDL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrderstoDispatchAssignment")]
        [SwaggerOperation("GetOrderstoDispatchAssignment")]
        public HttpResponseMessage GetOrderstoDispatchAssignment()
        {
            DispatchAssignmentDL dl = new DispatchAssignmentDL();
            var list = dl.GetOrderstoDispatchAssignment();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routesBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDispatchAssignmentData")]
        [SwaggerOperation("AddDispatchAssignmentData")]
        public HttpResponseMessage Post([FromBody]RoutesBO routesBO)
        {
            var routesdata = routes.UpdateRouteDataforDispatchAssignment(routesBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }
    }
}
