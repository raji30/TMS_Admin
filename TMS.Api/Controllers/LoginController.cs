using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TMS.Data;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using NonActionAttribute = System.Web.Mvc.NonActionAttribute;

namespace TMS.Api.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public string Token(string username, string password, string companyName)
        {
            UserAccessDL userAccessDL = new UserAccessDL();
          //  var isauth = userAccessDL.isAuthorized(username, companyName);
            if(true) { 
           var userInfo= userAccessDL.Login(username, password);
            if(userInfo == null)
            {
                return  "User not found!" ;
            }
              return JwtManager.GenerateToken(username);
            }
           
        }
        //[NonAction]
        //public JsonResult ForgotPassword(string username, string newPassword)
        //{
        //    UserAccessDL userAccessDL = new UserAccessDL();
        //    bool success = userAccessDL.resetPassword(username, newPassword);
        //    if (!success)
        //    {
        //        return new JsonResult() { Data = "User Not found!" };
        //    }
        //    else { 
        //    var jsonresult = new JsonResult()
        //    {
        //        Data ="Password Updated!"
               
        //    };
        //    return jsonresult;
        //    }
        //}
        }
    }
