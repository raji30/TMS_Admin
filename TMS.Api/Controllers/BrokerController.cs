using Swashbuckle.Swagger.Annotations;
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
        [HttpGet]
        [Route("GetAll")]
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage GetAll()
        {
            var boList = brokerBL.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, boList,
                Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("Post")]
        // POST api/values
        public HttpResponseMessage Post([FromBody]BrokerBO bo)
        {
           // bo = new BrokerBO(); bo.Address = new AddressBO();
            //bo.BrokerName = "MaerskLine";
            //bo.BrokerId = "ML0023";
            ////bo.Address.AddrKey = "00000000-0000-0000-0000-000000000000";
            //bo.Address.Address1 = "test address";
            //bo.Address.Address2 = "#1";
            //bo.Address.City = "oceania";
            //bo.Address.Country = null;
            //bo.Address.Email = "av@dfgf.com";
            //bo.Address.Fax = "35436457";
            //bo.Address.Name = "Winner";
            //bo.Address.Phone = null;
            //bo.Address.State = "MN";
            //bo.Address.Zip = "40562";

            bo = brokerBL.AddBroker(bo);
            return Request.CreateResponse(HttpStatusCode.OK, bo, Configuration.Formatters.JsonFormatter);
        }
    }
}
