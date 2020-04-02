using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.Api.Controllers
{
    public class UserPermissionsController : ApiController
    {

        // GET: api/User/5
        [System.Web.Http.HttpGet]
        [Route("getUserPermissionsByUserkey/{userKey}")]
        public HttpResponseMessage getUserPermissionsByUserkey(string userKey)
        {
            UserPermissionsDL BL = new UserPermissionsDL();
            var result = BL.getUserPermissionsByUserkey(userKey);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }


        [System.Web.Http.HttpGet]
        [Route("getMenus")]
        public HttpResponseMessage getMenus()
        {
            UserPermissionsDL BL = new UserPermissionsDL();
            var result = BL.getMenus();
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }

        [HttpPost]
        [Route("AddUserPermissions")]       
        public HttpResponseMessage Post([FromBody]UserPermissionsBO[] userPermissions)
        {
            UserPermissionsDL BL = new UserPermissionsDL();
            var result = BL.AddUserPermissions(userPermissions);
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateUserPermissions")]      
        public HttpResponseMessage Put([FromBody]UserPermissionsBO[] userPermissionList)
        {
            UserPermissionsDL BL = new UserPermissionsDL();
            foreach (var permission in userPermissionList)
            {
                if (permission.PermissionKey == Guid.Empty)
                {
                    var orderdetailCollection = BL.AddUserPermissions(permission);
                }
                else
                {
                    var orderdetailCollection = BL.UpdateUserPermissions(permission);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, true, Configuration.Formatters.JsonFormatter);
        }

    }
}
