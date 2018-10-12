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
        public JsonResult Get(Guid id)
        {
            UserOperationsBL userOperationsBL = new UserOperationsBL();
           var result= userOperationsBL.GetUser(id);
            return new JsonResult { Data = new { result } };

        }
       
        // POST: api/User
        public HttpResponseMessage Post([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
          bool result=  bll.AddUser(user);
            if (result)
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
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
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
