using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    public class RoutesController : ApiController
    {
        RoutesDL routes = new RoutesDL();

        [HttpPost]
        [Route("AddRoutes")]
        [SwaggerOperation("AddRoutes")]
        public HttpResponseMessage Post([FromBody]RoutesBO routesBO)
        {
            //RoutesBO routesBO1 = new RoutesBO();
            //routesBO1.OrderKey = Guid.Parse("33215b82-91a6-11e9-b6c9-3f0945f4a3b5");
            //routesBO1.OrderDetailKey = Guid.Parse("3ccfa396-91a6-11e9-b6ca-b37ef7da07e1");
            //routesBO1.driverkey = Guid.Parse("e20af418-9265-11e9-bdcb-2f0c27097902");
            var routesdata = routes.InsertRoutesDetails(routesBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }


    }
}
