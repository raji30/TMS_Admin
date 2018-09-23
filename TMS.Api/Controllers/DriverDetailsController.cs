using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TMS.Api.Controllers
{
    public class DriverDetailsController : ApiController
    {
        // GET: api/DriverDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DriverDetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DriverDetails
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DriverDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DriverDetails/5
        public void Delete(int id)
        {
        }
    }
}
