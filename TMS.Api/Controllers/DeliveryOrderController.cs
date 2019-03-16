using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
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
        public JsonResult Get(string OrderKey)
        {
            
           DeliveryOrderBO dorder= doObj.GetDeliveryOrder(OrderKey);
            return new JsonResult { Data = new { dorder } };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Delivery Order</param>
        /// <returns>GUID</returns>
        public JsonResult Post([FromBody]DeliveryOrderBO obj)
        {
           var orderid= doObj.CreateDeliveryOrder(obj);
            return new JsonResult { Data = new { orderId= orderid } };
        }

       
    }
}
