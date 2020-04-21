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
    [AllowAnonymous]
    //[JwtAuthentication]
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
        public HttpResponseMessage GetItems()
        {
            List<ItemBO> itemlList = dl.GetItems();           
            return Request.CreateResponse(HttpStatusCode.OK, itemlList, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetItemTypes")]
        [SwaggerOperation("GetItemTypes")]
        public HttpResponseMessage GetItemTypes()
        {
            List<ItemTypeBO> itemTypeList = dl.GetItemTypes();
            return Request.CreateResponse(HttpStatusCode.OK, itemTypeList, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        [Route("GetItemByKey/{itemkey}")]
        public HttpResponseMessage GetItemByKey(string itemkey)
        {
            ItemBO item = dl.GetItemByKey(Guid.Parse(itemkey));           

            if (item != null)
            {               
                return Request.CreateResponse(HttpStatusCode.OK, item, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddItem")]
        [SwaggerOperation("AddItem")]
        // POST api/values
        public HttpResponseMessage AddItem([FromBody]ItemBO item)
        {      
            Guid itemid = dl.InsertItem(item);

            if (itemid != null && itemid != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, itemid, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("UpdateItem")]
        public HttpResponseMessage UpdateItem([FromBody]ItemBO item)
        {         

            bool result = dl.UpdateItem(item);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
