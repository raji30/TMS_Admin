using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;
using TMS.Data;

namespace TMS.BusinessLayer.Security
{
  public  class UserAccessFacade
    {
        UserAccessDL useraccess = new UserAccessDL();
        
        public LoginResult VerifyLogin(string userName, string passWord)
        {
           
            LoginResult result = new LoginResult();
           var userInfoobj= useraccess.Login(userName, passWord);
            if (userInfoobj != null)
            {
                if (userInfoobj.loginattempts <= 3)
                {
                    result.isLoggedIn = true;
                    result.loggedinTime = userInfoobj.lastlogindate.ToString();
                    result.message = "success";
                }
                else if (userInfoobj.loginattempts > 3)
                {
                    result.isLoggedIn = false;
                    result.message = "Account Locked! Please contact administrator!";
                }
            }
            else
            {
                result.isLoggedIn = false;
                result.message = "Account not found!";
            }
            return result;
        }
        public bool IsAuthorized(string userName, string company)
        {
            return useraccess.isAuthorized(userName,company);
        }

        
    }
}
