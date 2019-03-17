using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TMS.BusinessLayer;
using TMS.BusinessObjects;


namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class UserController : ApiController
    {
        // GET: api/User/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Get")]
        public JsonResult Get(string userName)
        {
            UserOperationsBL userOperationsBL = new UserOperationsBL();
            var result= userOperationsBL.GetUser(userName);
            var jsonresult = new JsonResult { Data = new { result } };
            return jsonresult;
        }
       
        // POST: api/User
        [System.Web.Http.HttpPost]
        public JsonResult Post([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
          bool result=  bll.AddUser(user);
            if (result)
            {
                return new JsonResult { Data = new { StatusCode = 200 } };
            }
            else
                return new JsonResult { Data= new { StatusCode= 500 } };
        }

        // PUT: api/User/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("Update")]
        public JsonResult Put([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
            bool result = bll.UpdateUser(user);
            if (result)
            {
                return new JsonResult { Data = new { StatusCode = 200 } };
            }
            else
                return new JsonResult { Data = new { StatusCode = 500 } };
        }

       
    }
}
