using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TMS.BusinessLayer;
using TMS.BusinessObjects;


namespace TMS.Api.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User/5
        [System.Web.Http.HttpGet]
        public JsonResult Get(string userName)
        {
            UserOperationsBL userOperationsBL = new UserOperationsBL();
            var result= userOperationsBL.GetUser(userName);
            var jsonresult = new JsonResult { Data = new { result } };
            return jsonresult;
        }
       
        // POST: api/User
        public HttpResponseMessage Post([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
          bool result=  bll.AddUser(user);
            if (result)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // PUT: api/User/5
        public HttpResponseMessage Put([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
            bool result = bll.UpdateUser(user);
            if (result)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

       
    }
}
