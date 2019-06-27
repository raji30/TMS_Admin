using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer.Common;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
   // [JwtAuthentication]
    public class DeliveryOrderDetailsController : ApiController
    {
        DeliveryOrderDL doObj = new DeliveryOrderDL();

        [HttpGet]
        [Route("GetDeliveryOrderDetail/{OrderKey}")]
       // [SwaggerOperation("Get")]
       public HttpResponseMessage Get(string Orderkey)
        {
            var list = doObj.GetOrderDetails(Orderkey);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        [Route("DeliveryOrderDetails")]
        [SwaggerOperation("DeliveryOrderDetails")]        
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

        [HttpPut]
        [Route("UpdateDeliveryOrderDetails")]
        [SwaggerOperation("UpdateDeliveryOrderDetails")]
        public HttpResponseMessage Put([FromBody]DeliveryOrderDetailBO objList)
        {
            var orderdetailCollection = doObj.UpdateOrderDetails(objList);
            return Request.CreateResponse(HttpStatusCode.OK, orderdetailCollection, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetDrivers")]
        public HttpResponseMessage GetDriversList()
        {
            DriverRepository driverRepository = new DriverRepository();
            var enumerable = driverRepository.GetAll().ToList();
            var list = new List<DriverBO>();
            enumerable.ForEach(d =>
            {
                list.Add(new DriverBO
                {
                    FirstName = d.firstname,
                    LastName = d.lastname,
                    DriverId = d.driverid,
                    DriversLicenseNo = d.drivinglicenseno,
                    DriverKey = d.driverkey,
                    CarrierKey = d.carrierkey.Value,
                    LicenseExpiryDate = d.drivinglicenseexpirydate.Value,

                });
            });
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
    }
}
