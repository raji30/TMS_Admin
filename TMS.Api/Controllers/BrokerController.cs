using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    public class BrokerController : ApiController
    {
        BrokerRepository repo = new BrokerRepository();
        [HttpGet]
        public HttpResponseMessage GetbyName(string name)
        {
            var broker = repo.GetbyField(name);
            BrokerBO bo = new BrokerBO()
            {
                BrokerName = broker.brokername,
                BrokerId = broker.brokerid,
                BrokerKey = broker.brokerkey,
                Address = new DeliveryOrderDL().GetAddress(broker.address.addrkey)
            };
            return Request.CreateResponse(HttpStatusCode.OK, bo,
                Configuration.Formatters.JsonFormatter);
        }
        //[HttpPost]
        //// POST api/values
        //public HttpResponseMessage Post([FromBody]BrokerBO bo)
        //{
        //    broker b = new broker ()
        //    {
        //      brokername = bo.BrokerName,
        //      brokerid = bo.BrokerId,
        //      address = bo.Address
        //    }
        //    var value = repo.Add(add);
        //    return Request.CreateResponse(HttpStatusCode.OK, value);
        //}
    }
}
