using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.Data;
namespace TMS.Api.Controllers
{
       public class AddressController : ApiController
    {
        AddressRepo repo = new AddressRepo();
        // GET api/values
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var list = repo.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        // GET api/values/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var value = repo.FindById(id);
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody]address add)
        {
            var value = repo.Add(add);
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpPost,HttpPut]
        // PUT api/values/5
        public HttpResponseMessage Put([FromBody]address value)
        {
            var updated = repo.Update(value);
            return Request.CreateResponse(HttpStatusCode.OK, updated);
        }
        [HttpDelete]
        // DELETE api/values/5
        public HttpResponseMessage Delete([FromBody] address value)
        {
            var todelete = repo.Remove(value);
            return Request.CreateResponse(HttpStatusCode.OK, todelete);
        }
    }
}
