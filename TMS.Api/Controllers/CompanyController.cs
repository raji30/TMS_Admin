using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using Newtonsoft.Json;
namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class CompanyController : ApiController
    {
        // GET: api/Company
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        // GET: api/Company/5
        public HttpResponseMessage Get(string companyName)
        {
            CompanyDetailsBL bll = new CompanyDetailsBL();
            CompanyDetailBO result = bll.GetCompany(companyName);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Data not Found", Configuration.Formatters.JsonFormatter);
        }
        [HttpPost]
        // POST: api/Company
        public HttpResponseMessage Post([FromBody]CompanyDetailBO detailsBO)
        {
           CompanyDetailsBL bll = new CompanyDetailsBL();
            Guid result = bll.AddCompany(detailsBO);
            if (result!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,result, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        // PUT: api/Company/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Company/5
        //public void Delete(int id)
        //{
        //}
    }
}
