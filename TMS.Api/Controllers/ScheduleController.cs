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
    [JwtAuthentication]
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


        [HttpGet]
        [Route("GetOrderstoSchedule")]
        [SwaggerOperation("GetOrderstoSchedule")]
        public HttpResponseMessage GetOrderstoSchedule()
        {            
            SchedulingDL dl = new SchedulingDL();
            var list = dl.GetOrderstoSchedule();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);

        }
        [HttpGet]
        [Route("GetOrderDetailsbykey/{orderdetailkey}")]
        [SwaggerOperation("GetOrderDetailsbykey")]
        public HttpResponseMessage GetOrderDetailsbykey(string orderdetailkey)
        {
            SchedulingDL dl = new SchedulingDL();
            var list = dl.GetOrderDetailsbykey(orderdetailkey);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);

        }
        [HttpGet]
        [Route("GetScheduledContainers")]
        [SwaggerOperation("GetScheduledContainers")]
        public HttpResponseMessage GetScheduledContainers()
        {
            SchedulingDL dl = new SchedulingDL();
            var list = dl.GetScheduledContainers();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [SwaggerOperation("GetScheduledContainer")]
        [Route("GetScheduledContainer/{orderdetailkey}")]
        public HttpResponseMessage GetScheduledContainerdata(string orderdetailkey)
        {
            SchedulingDL dl = new SchedulingDL();
            AccountingDL obj = new AccountingDL();
            var data = dl.GetScheduledContainer(orderdetailkey);
            if(data != null)
            {
                List<AccountingBO> dorder = obj.GetAccountingOptionsbyKey(orderdetailkey);
                data.accountingBO = dorder;
            }           
            return Request.CreateResponse(HttpStatusCode.OK, data, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateScheduler")]
        [SwaggerOperation("UpdateScheduler")]
        public HttpResponseMessage Put([FromBody]DeliveryOrderDetailBO schedulerData)
        {
            SchedulingDL dl = new SchedulingDL();
            var orderdetailCollection = dl.UpdateScheduler(schedulerData);
                
            return Request.CreateResponse(HttpStatusCode.OK, orderdetailCollection, Configuration.Formatters.JsonFormatter);
        }


    }
}