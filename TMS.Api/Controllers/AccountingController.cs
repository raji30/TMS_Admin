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
    public class AccountingController : ApiController
    {
        AccountingDL obj = new AccountingDL();

        [HttpGet]
        [Route("GetItemsbyType/{itemtype}")]
        [SwaggerOperation("itemtype")]

        public HttpResponseMessage GetItemsbyType(int itemtype)
        {            
            List<AccountingBO> dorder = obj.GetItemsbyType(itemtype);
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        [Route("AddAccountingOptions")]
        [SwaggerOperation("AddAccountingOptions")]
        public HttpResponseMessage Post([FromBody]AccountingBO[] objList)
        {
            var orderdetailCollection = obj.InsertAccountingOptions(objList.ToList());
            return Request.CreateResponse(HttpStatusCode.OK, orderdetailCollection, Configuration.Formatters.JsonFormatter);
        }
    }
}
