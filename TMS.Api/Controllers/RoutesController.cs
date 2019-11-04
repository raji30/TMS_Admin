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
        public HttpResponseMessage Post([FromBody]RoutesBO objList)
        {
            DispatchAssignmentDL dl = new DispatchAssignmentDL();
            var list = routes.AddRoutes(objList);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

    }
}
