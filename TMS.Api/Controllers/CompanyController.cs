using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using Newtonsoft.Json;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [AllowAnonymous]
   // [JwtAuthentication]
    public class CompanyController : ApiController
    {
        CompanyDL DL = new CompanyDL();
        // GET: api/Company
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        //[HttpGet]
        //// GET: api/Company/5
        //public HttpResponseMessage Get(string companyName)
        //{
        //    CompanyDetailsBL bll = new CompanyDetailsBL();
        //    CompanyDetailBO result = bll.GetCompany(companyName);
        //    if (result != null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        //    }
        //    else
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Data not Found", Configuration.Formatters.JsonFormatter);
        //}
        // [HttpPost]
        // POST: api/Company
        //public HttpResponseMessage Post1([FromBody]CompanyDetailBO detailsBO)
        //  {
        //CompanyDetailsBL bll = new CompanyDetailsBL();
        // if (detailsBO.Address != null)
        // {
        //     var addrkey = new AddressDL().InsertAddress(detailsBO.Address);               
        //     detailsBO.addrkey = addrkey;
        // }
        // Guid result = bll.AddCompany(detailsBO);
        // if (result!=null)
        // {
        //     return Request.CreateResponse(HttpStatusCode.OK,result, Configuration.Formatters.JsonFormatter);
        // }
        // else
        //     return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //  }


        [HttpGet]
        // GET: api/Customer/5
        [Route("GetCompanies")]
        public HttpResponseMessage GetCompanies()
        {       
            List<CompanyDetailBO> company = DL.GetCompanies();
          List<CompanyDetailBO> companyBOList = new List<CompanyDetailBO>();

            if (company.Count > 0)
            {
                foreach (var comp in company)
                {
                    CompanyDetailBO companyBO = new CompanyDetailBO();
                    companyBO.compkey = comp.compkey;
                    companyBO.compid = comp.compid;
                    companyBO.compname = comp.compname;                 

                    var address = new AddressRepository().GetbyId(comp.addrkey);
                    companyBO.Address = new AddressBO()
                    {
                        AddrKey = address.addrkey,
                        Address1 = address.address1,
                        Address2 = address.address2,
                        City = address.city,
                        State = address.state,
                        Zip = address.zipcode,
                        Email = address.email,
                        Phone = address.phone,
                        Fax = address.fax,
                        Website = address.website
                    };
                    companyBOList.Add(companyBO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, companyBOList, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="custkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCompanyByID/{compkey}")]
        public HttpResponseMessage GetCompanyByID(string compkey)
        {

            CompanyDetailBO customer = DL.GetCompanyDetailsbykey(Guid.Parse(compkey));
            if (customer != null)
            {                
                var address = new AddressDL().GetAddressByKey(customer.addrkey);
                customer.Address = new AddressBO()
                {
                    AddrKey = address.AddrKey,
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    City = address.City,
                    State = address.State,
                    Zip = address.Zip,
                    Email = address.Email,
                    Phone = address.Phone,
                    Fax = address.Fax,
                    Country = address.Country,
                    Website = address.Website,
                    Phone2 = address.Phone2,
                    Email2 = address.Email2
                };

                return Request.CreateResponse(HttpStatusCode.OK, customer, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]       
        [Route("CreateCompany")]
        public HttpResponseMessage Post([FromBody] CompanyDetailBO company)
        {
            AddressDL addr = new AddressDL();

            if (company.Address != null)
            {
                var addrkey = new AddressDL().InsertAddress(company.Address);
                company.addrkey = addrkey;
            }
            var compkey = DL.insertCompany(company);

            // Guid custkey =repo.Add(_customer);
            if (compkey != null && compkey != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, compkey, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]      
        [Route("UpdateCompany")]
        public HttpResponseMessage Put([FromBody]CompanyDetailBO company)
        {            
           if (company.Address != null)
            {               
                AddressDL addr = new AddressDL();
                bool updated = addr.UpdateAddress(company.Address);
            }          
            bool result = DL.updateCompany(company);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }


    }
}
