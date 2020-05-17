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
    [AllowAnonymous]
    //[JwtAuthentication]
    public class RateSheetController : ApiController
    {
        RateSheetDL dl = new RateSheetDL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRates")]
        [SwaggerOperation("GetRates")]
        public HttpResponseMessage GetRates()
        {
            List<RateSheetBO> List = dl.GetRates();
            return Request.CreateResponse(HttpStatusCode.OK, List, Configuration.Formatters.JsonFormatter);
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRateByCustomer/{customerkey}")]
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
        [Route("AddRate")]
        [SwaggerOperation("AddRate")]
        // POST api/values
        public HttpResponseMessage AddRate([FromBody]RateSheetBO rate)
        {
            bool itemid = dl.AddRate(rate);

            if (itemid != false )
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
        [Route("UpdateRate")]
        public HttpResponseMessage UpdateRate([FromBody]RateSheetBO[] rate)
        {

            bool result = dl.UpdateRate(rate);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}

