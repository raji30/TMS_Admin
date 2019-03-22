using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public HttpResponseMessage Get(string userName)
        {
            UserOperationsBL userOperationsBL = new UserOperationsBL();
            var result= userOperationsBL.GetUser(userName);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK,
                    result, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
        }
       
        // POST: api/User
        [HttpPost]
        public HttpResponseMessage Post([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
          bool result=  bll.AddUser(user);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   result, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"User not added!");
        }

        // PUT: api/User/5
        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Put([FromBody]UserDetailsBO user)
        {
            UserOperationsBL bll = new UserOperationsBL();
            bool result = bll.UpdateUser(user);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   result, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "User update failed!");
        }

       
    }
}
