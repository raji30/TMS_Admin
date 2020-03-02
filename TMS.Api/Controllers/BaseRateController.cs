using System;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class BaseRateController : ApiController
    {
        BaseRateDL dl = new BaseRateDL();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBaseRates")]
        [SwaggerOperation("GetBaseRates")]
        public HttpResponseMessage GetRates()
        {
            List<BaseRateBO> List = dl.GetRates();
            return Request.CreateResponse(HttpStatusCode.OK, List, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBaseRateByCustomer/{customerkey}")]
        public HttpResponseMessage GetRateByCustomer(string customerkey)
        {
            var rate = dl.GetRateByCustomer(Guid.Parse(customerkey));

            if (rate != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, rate, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBaseRate")]
        [SwaggerOperation("AddBaseRate")]
        // POST api/values
        public HttpResponseMessage AddRate([FromBody]BaseRateBO[] rate)
        {
           bool itemid=false;
            foreach (var obj in rate)
            {
                if(obj.baseratekey == Guid.Empty)
                {
                    itemid = dl.AddRate(obj);
                }
                else
                {
                    itemid = dl.UpdateRate(obj);
                }
            }           

            if (itemid)
                return Request.CreateResponse(HttpStatusCode.OK, itemid, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateBaseRate")]
        public HttpResponseMessage UpdateRate([FromBody]BaseRateBO[] rate)
        {

            bool result = dl.UpdateRate(rate);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
