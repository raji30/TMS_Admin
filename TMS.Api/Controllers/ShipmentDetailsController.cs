using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TMS.Api.Controllers
{
    public class ShipmentDetailsController : ApiController
    {
        // GET: api/ShipmentDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ShipmentDetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ShipmentDetails
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ShipmentDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShipmentDetails/5
        public void Delete(int id)
        {
        }
    }
}
