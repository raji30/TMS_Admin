using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessLayer.Common;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;
using static TMS.BusinessObjects.Enums;

namespace TMS.Api.Controllers
{
    // [JwtAuthentication]
    [RoutePrefix("api/address")]
    public class AddressController : ApiController
    {
        AddressRepository repo = new AddressRepository();

        [HttpGet]
        [SwaggerOperation("GetAddressTypes")]
        [Route("GetAddressTypes")]
        public HttpResponseMessage GetAddressTypes()
        {
            var list = EnumExtensions.GetEnumValues<AddressType>();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpGet]
        [SwaggerOperation("GetAddressById")]
        [Route("GetAddressById/{Id:int}")]
        public HttpResponseMessage GetAddressById(string Id)
        {
            var address = repo.GetbyId(Guid.Parse(Id));
            return Request.CreateResponse(HttpStatusCode.OK, address);
        }

        [HttpGet]
        [SwaggerOperation("GetAllByType")]
        [Route("GetAllByType")]
        public HttpResponseMessage GetAllByType(int AddressType)
        {
            AddressBL blobj = new AddressBL();
          var list=  blobj.GetAddressesByType(AddressType);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpGet]
        [SwaggerOperation("GetByName")]
        [Route("GetByName")]
        public HttpResponseMessage GetbyName(string name)
        {
            var address = repo.GetbyField(name);
            if(address!=null) { 
            AddressBO bo = new AddressBO()
            {
                Address1 = address.address1,
                Address2 = address.address2,
                City = address.city,
                State = address.state,
                Zip = address.zipcode,
                Email = address.email,
                Fax = address.fax,
                Phone = address.phone,
                Name = address.addrname
            };
            return Request.CreateResponse(HttpStatusCode.OK, bo, 
                Configuration.Formatters.JsonFormatter);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, "Not found");
        }
        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody]address add)
        {
            var value = repo.Add(add);
            return Request.CreateResponse(HttpStatusCode.OK, value, Configuration.Formatters.JsonFormatter);
        }
        //[HttpPut]
        //// PUT api/values/5
        //public HttpResponseMessage Put([FromBody]address value)
        //{
        //    var updated = repo.Update(value);
        //    return Request.CreateResponse(HttpStatusCode.OK, updated);
        //}
        
    }
}
