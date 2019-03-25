using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class AddressController : ApiController
    {
        AddressRepository repo = new AddressRepository();

        [HttpGet]
        [SwaggerOperation("GetAll")]
        [Route("GetAll")]
        public HttpResponseMessage Get()
        {
            var list = repo.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpGet]
        [SwaggerOperation("GetByName")]
        [Route("GetByName")]
        public HttpResponseMessage GetbyName(string name)
        {
            var address = repo.GetbyField(name);
            AddressBO bo = new AddressBO()
            {
                Address1 = address.address1,
                Address2 = address.address2,
                City = address.city,
                State = address.state,
                Zip = address.zipcode,
                Email = address.email,
                Fax = address.fax,
                Phone = address.phone
            };
            return Request.CreateResponse(HttpStatusCode.OK, bo, 
                Configuration.Formatters.JsonFormatter);
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
