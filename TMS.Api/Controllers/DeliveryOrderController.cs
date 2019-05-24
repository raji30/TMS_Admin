using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using TMS.BusinessLayer.Common;
using TMS.BusinessObjects;
using TMS.Data;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    //[JwtAuthentication]
    public class DeliveryOrderController : ApiController
    {
        DeliveryOrderDL doObj = new DeliveryOrderDL();
        /// <summary>
        /// Get Delivery order by Order Key
        /// </summary>
        /// <param name="OrderKey"></param>
        /// <returns>Delivery Order </returns>
        [HttpGet]
        [Route("GetbyKey/{OrderKey}")]
        [SwaggerOperation("GetbyKey")]
       
        public HttpResponseMessage GetbyKey(string OrderKey)
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
        /// <param name="UserKey"></param>
        /// <returns>IEnumerable of DOs</returns>
        [HttpGet]
        [Route("GetOrders")]
        [SwaggerOperation("GetOrders")]
        public HttpResponseMessage GetOrdersr()
        {
          //  IEnumerable<string> dorders = doObj.GetOrders();
            List<DeliveryOrderBO> dorder = doObj.GetOrders();
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        ///<returns>name/value pair</returns>
        [HttpGet]
        [Route("GetAllDOStatus")]
        [SwaggerOperation("GetAllDOStatus")]
        public HttpResponseMessage GetallDOStatus()
        {

          List<EnumValue> values=  EnumExtensions.GetEnumValues<DOStatus>();
            return Request.CreateResponse(HttpStatusCode.OK, values, Configuration.Formatters.JsonFormatter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Delivery Order</param>
        /// <returns>GUID</returns>
        [HttpPost]
        [Route("DeliveryOrderHeader1")]
        [SwaggerOperation("DeliveryOrderHeader")]
        public HttpResponseMessage Post([FromBody]int q)
        {
            //var orderid = doObj.CreateDeliveryOrder(obj);
            return Request.CreateResponse(HttpStatusCode.OK, q, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("DeliveryOrderHeader")]
        [SwaggerOperation("DeliveryOrderHeader")]
        public HttpResponseMessage Post([FromBody]DeliveryOrderBO obj)
        {
            var orderid = doObj.CreateDeliveryOrder(obj);
            return Request.CreateResponse(HttpStatusCode.OK, orderid, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("UpdateDeliveryOrderStatus")]
        [SwaggerOperation("UpdateDeliveryOrderStatus")]
        public HttpResponseMessage UpdateOrderStatus(string OrderKey, int Status)
        {           
            bool result=  doObj.UpdateDOStatus(OrderKey, Status, HttpContext.Current.User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        ///<returns>name/value pair</returns>
        [HttpGet]
        [Route("GetallPriority")]
        [SwaggerOperation("GetallPriority")]
        public HttpResponseMessage GetallPriority()
        {
            List<EnumValue> values = EnumExtensions.GetEnumValues<Priority>();
            return Request.CreateResponse(HttpStatusCode.OK, values, Configuration.Formatters.JsonFormatter);
        }

    }
}
