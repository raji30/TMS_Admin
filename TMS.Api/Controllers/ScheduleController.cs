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
    public class ScheduleController : ApiController
    {
        // GET: Schedule
        [HttpGet]
        [Route("GetSchedule")]
        [SwaggerOperation("GetSchedule")]
        public HttpResponseMessage GetSchedule(string orderKey)
        {
            if (string.IsNullOrEmpty(orderKey))
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            SchedulingDL dl = new SchedulingDL();
           var list= dl.GetSchedulingDetails(Guid.Parse(orderKey));
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);

        }

        [HttpPost]
        [Route("PostSchedule")]
        [SwaggerOperation("PostSchedule")]
        public HttpResponseMessage PostSchedule(ScheduleOrderBO orderBO)
        {
           
            SchedulingDL dl = new SchedulingDL();
            var routeKey = dl.InsertSchedule(orderBO);
            return Request.CreateResponse(HttpStatusCode.OK, routeKey, Configuration.Formatters.JsonFormatter);

        }
    }
}