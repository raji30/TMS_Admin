using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class DeliveryOrderController : ApiController
    {
        DeliveryOrderDL doObj = new DeliveryOrderDL();
        /// <summary>
        /// Get Delivery order by Order Key
        /// </summary>
        /// <param name="OrderKey"></param>
        /// <returns>Delivery Order </returns>
        [HttpGet]
        [Route("GetbyKey")]
        [SwaggerOperation("GetbyKey")]
        public HttpResponseMessage Get(string OrderKey)
        {
            
           DeliveryOrderBO dorder= doObj.GetDeliveryOrder(OrderKey);
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserKey"></param>
        /// <returns>IEnumerable of DOs</returns>
        [HttpGet]
        [Route("GetOrdersByUser")]
        [SwaggerOperation("GetOrdersByUser")]
        public HttpResponseMessage GetOrdersByUser(string UserKey)
        {

            IEnumerable<string> dorders = doObj.GetOrdersByUser(Guid.Parse(UserKey));
            return Request.CreateResponse(HttpStatusCode.OK, dorders, Configuration.Formatters.JsonFormatter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Delivery Order</param>
        /// <returns>GUID</returns>
        [HttpPost]
        [Route("DeliveryOrderHeader")]
        [SwaggerOperation("DeliveryOrderHeader")]
        public HttpResponseMessage Post([FromBody]DeliveryOrderBO obj)
        {
           var orderid= doObj.CreateDeliveryOrder(obj);
            return Request.CreateResponse(HttpStatusCode.OK, orderid, Configuration.Formatters.JsonFormatter);
        }

        
    }
}
