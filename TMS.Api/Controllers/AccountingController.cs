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


        [HttpGet]
        [Route("GetAccountingOptionsbyKey/{orderDetailKey}")]
        [SwaggerOperation("orderDetailKey")]

        public HttpResponseMessage GetAccountingOptionsbyKey(string orderDetailKey)
        {
            List<AccountingBO> dorder = obj.GetAccountingOptionsbyKey(orderDetailKey);
            return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateAccountingOptions/{orderdetailkey}")]
        [SwaggerOperation("UpdateAccountingOptions")]
        public HttpResponseMessage Put(string orderdetailkey)
        {
            bool Isupdated = obj.UpdateAccountingOptions(orderdetailkey);

            if (Isupdated)
                return Request.CreateResponse(HttpStatusCode.OK, Isupdated, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
