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
    [AllowAnonymous]
    public class AddressController : ApiController
    {   
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
        [Route("GetAddressById")]
        public HttpResponseMessage GetAddressById(string Id)
        {
            AddressRepository repo = new AddressRepository();
            var address = repo.GetbyId(Guid.Parse(Id));
            return Request.CreateResponse(HttpStatusCode.OK, address);
        }

        [HttpGet]
        [SwaggerOperation("GetAllByType")]
        [Route("GetAllByType/{AddressType}")]
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
            AddressRepository repo = new AddressRepository();
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
                Email2=address.email2,
                Fax = address.fax,
                Phone = address.phone,
                Phone2 = address.phone2,
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
            AddressRepository repo = new AddressRepository();
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
