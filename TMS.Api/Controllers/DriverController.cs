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
{
    public class DriverController : ApiController
    {
        DriverDL routes = new DriverDL();

        [HttpPost]
        [Route("AddDriver")]
        [SwaggerOperation("AddDriver")]
        public HttpResponseMessage Post([FromBody]DriverBO[] driverBO)
        {
            var routesdata = routes.InsertDriverDetails(driverBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet]
        [Route("GetDrivers")]
        [SwaggerOperation("GetDrivers")]
        public HttpResponseMessage GetOrdersr()
        {
            //  IEnumerable<string> dorders = doObj.GetOrders();
            List<DriverBO> dorder = routes.GetDrivers();
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

    }
}
