using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    public class BrokerController : ApiController
    {
        BrokerBL brokerBL = new BrokerBL();
        [HttpGet]
        [Route("Get")]
        public HttpResponseMessage GetbyName(string name)
        {
           
           var bo= brokerBL.GetBroker(name);
            return Request.CreateResponse(HttpStatusCode.OK, bo,
                Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("Post")]
        // POST api/values
        public HttpResponseMessage Post([FromBody]BrokerBO bo)
        {
            bo= brokerBL.AddBroker(bo);
            return Request.CreateResponse(HttpStatusCode.OK, bo, Configuration.Formatters.JsonFormatter);
        }
    }
}
