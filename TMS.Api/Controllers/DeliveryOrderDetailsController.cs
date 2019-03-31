﻿using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer.Common;
using TMS.BusinessObjects;
using TMS.Data;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class DeliveryOrderDetailsController : ApiController
    {
        DeliveryOrderDL doObj = new DeliveryOrderDL();

        [HttpGet]
       // [Route("GetDeliveryOrderDetail")]
       // [SwaggerOperation("Get")]
       public HttpResponseMessage Get(string Orderkey)
        {
            var list = doObj.GetOrderDetails(Guid.Parse(Orderkey));
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]

        // [SwaggerOperation("DeliveryOrderDetails")]
        public HttpResponseMessage Post([FromBody]DeliveryOrderDetailBO[] objList)
        {
            var orderdetailCollection = doObj.InsertOrderDetails(objList.ToList());
            return Request.CreateResponse(HttpStatusCode.OK, orderdetailCollection, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetContainerSizes")]
        [SwaggerOperation("GetContainerSizes")]
        public HttpResponseMessage GetContainerSizes()
        {
            var list = EnumExtensions.GetEnumValues<ContainerSize>();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
    }
}
