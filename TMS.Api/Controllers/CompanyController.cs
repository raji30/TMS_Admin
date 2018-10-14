using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using Newtonsoft.Json;
namespace TMS.Api.Controllers
{
    public class CompanyController : ApiController
    {
        // GET: api/Company
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Company/5
        public JsonResult Get(string companyName)
        {
            CompanyDetailsBL bll = new CompanyDetailsBL();
            CompanyDetailBO result = bll.GetCompany(companyName);
            if (result != null)
            {
                return new JsonResult { Data = new { result } };
            }
            else
                return new JsonResult {  Data = "Not found"  };
        }

        // POST: api/Company
        public HttpResponseMessage Post([FromBody]CompanyDetailBO detailsBO)
        {
           CompanyDetailsBL bll = new CompanyDetailsBL();
            Guid result = bll.AddCompany(detailsBO);
            if (result!=null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
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
