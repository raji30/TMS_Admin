using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    public class CarrierController : ApiController
    {
        [HttpGet]
        [Route("GetCarrier")]
        [SwaggerOperation("GetCarrier")]
        public HttpResponseMessage GetCarrier()
        {
            CarrierDL DL = new CarrierDL();
            List<CarrierBO> carrierlist = DL.GetCarriers();
            return Request.CreateResponse(HttpStatusCode.OK, carrierlist, Configuration.Formatters.JsonFormatter);
        }
    }
}
