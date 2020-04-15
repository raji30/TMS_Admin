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
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
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
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
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
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
            var result = BL.AddUserPermissions(userPermissions);
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateUserPermissions")]      
        public HttpResponseMessage Put([FromBody]UserPermissionsBO[] userPermissionList)
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
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

        [System.Web.Http.HttpGet]
        [Route("getRoles")]
        public HttpResponseMessage getRoles()
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
            var result = BL.getRoles();
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }

        [System.Web.Http.HttpGet]
        [Route("getUserRoleByRolekey/{RoleKey}")]
        public HttpResponseMessage getUserRoleByRolekey(string RoleKey)
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
            var result = BL.getUserRoleByRolekey(RoleKey);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }
        

            [System.Web.Http.HttpGet]
        [Route("getUserRoleByUserkey/{UserKey}")]
        public HttpResponseMessage getUserRoleByUserkey(string UserKey)
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
            var result = BL.getUserRoleByUserkey(UserKey);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }


        [HttpPost]
        [Route("AddUserRole")]
        public HttpResponseMessage AddUserRole([FromBody]UserRoleBO userRole)
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();
            var result = BL.AddUserRole(userRole);
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPut]
        [Route("UpdateUserRole")]
        public HttpResponseMessage UpdateUserRole([FromBody]UserRoleBO userRole)
        {
            UserRoleAndPermissionsDL BL = new UserRoleAndPermissionsDL();

            var orderdetailCollection = BL.UpdateUserRole(userRole);                
            
            return Request.CreateResponse(HttpStatusCode.OK, true, Configuration.Formatters.JsonFormatter);
        }



    }
}
