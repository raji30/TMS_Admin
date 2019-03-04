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
    public class DeliveryOrderController : ApiController
    {
        DeliveryOrderDL doObj = new DeliveryOrderDL();
        // GET: api/DO
        public JsonResult Get(string OrderKey)
        {
            
           var dorder= doObj.GetDeliveryOrder(OrderKey);
            return new JsonResult { Data = new { dorder } };
        }

        // GET: api/DO/5
       

        // POST: api/DO
        public JsonResult Post([FromBody]DeliveryOrderBO obj)
        {
           var orderid= doObj.CreateDeliveryOrder(obj);
            return new JsonResult { Data = new { orderId= orderid } };
        }

       
    }
}
