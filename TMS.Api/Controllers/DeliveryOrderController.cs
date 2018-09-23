using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TMS.Api.Controllers
{
    public class DeliveryOrderController : ApiController
    {
        // GET: api/DO
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DO/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DO
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DO/5
        public void Delete(int id)
        {
        }
    }
}
