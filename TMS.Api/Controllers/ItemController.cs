using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class ItemController : ApiController
    {
        ItemDL dl = new ItemDL();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetItems")]
        [SwaggerOperation("GetItems")]
        public HttpResponseMessage GetCarrier()
        {
            List<ItemBO> itemlList = dl.GetItems();           
            return Request.CreateResponse(HttpStatusCode.OK, itemlList, Configuration.Formatters.JsonFormatter);
        }
    }
}
