using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using TMS.BusinessObjects;
using TMS.Data;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using NonActionAttribute = System.Web.Mvc.NonActionAttribute;

namespace TMS.Api.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Token")]
        public LoginResult Token(string username, string password, string companyName)
        {
            UserAccessDL userAccessDL = new UserAccessDL();
            var result = new LoginResult();
            //  var isauth = userAccessDL.isAuthorized(username, companyName);
            if (true) {
                var userInfo = userAccessDL.Login(username, password);
                if (userInfo == null)
                {
                    result.message = "user not found";
                    result.isLoggedIn = false;
                    result.token = string.Empty;
                }
                else
                {
                    result.message = "success";
                    result.token = JwtManager.GenerateToken(username);
                    result.loggedinTime = Convert.ToString(userInfo.lastlogindate.Value);
                    result.isLoggedIn = true;
                    result.userId = Convert.ToString(userInfo.userkey);
                }
                return result;
            }

        }
        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public string ResetPassword(string username, string newPassword)
        {
            UserAccessDL userAccessDL = new UserAccessDL();
            bool success = userAccessDL.resetPassword(username, newPassword);
            if (!success)
            {
                return  "User Not found!" ;
            }
            else
            {
                return "Password Updated!";
            }
        }
    }
    }
