using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Data;

namespace TMS.Api.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public JsonResult Login(string username, string password, string companyName)
        {
            UserAccessDL userAccessDL = new UserAccessDL();
            var isauth = userAccessDL.isAuthorized(username, companyName);
            if(isauth) { 
           var userInfo= userAccessDL.Login(username, password);
            if(userInfo == null)
            {
                return new JsonResult() { Data = "Not Authenticated!" };
            }
            var jsonresult = new JsonResult { Data = new { firstName = userInfo.firstname,
                lastName = userInfo.lastname,
                userInfo.userid}
            };
            return jsonresult;
            }
            return new JsonResult() { Data = "Unauthorized!" };
        }

        public JsonResult ForgotPassword(string username, string newPassword)
        {
            UserAccessDL userAccessDL = new UserAccessDL();
            bool success = userAccessDL.resetPassword(username, newPassword);
            if (!success)
            {
                return new JsonResult() { Data = "User Not found!" };
            }
            else { 
            var jsonresult = new JsonResult()
            {
                Data ="Password Updated!"
               
            };
            return jsonresult;
            }
        }
        }
    }
