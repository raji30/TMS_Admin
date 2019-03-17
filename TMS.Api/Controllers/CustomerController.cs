using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TMS.BusinessObjects;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        CustomerRepository repo = new CustomerRepository();
        

        // GET: api/Customer/5
        public JsonResult Get(string name)
        {
           Data.customer customer= repo.GetbyField(name);
            if (customer != null)
            {
                CustomerBO customerBO = new CustomerBO();
                customerBO.CustId = customer.custid;
                customerBO.CustName = customer.custname;
                customerBO.CustomerGroup = customer.customergroup;
                customerBO.CreditLimit = customer.creditlimit;
                customerBO.CreditStatus = customer.creditstatus;
                return new JsonResult { Data = new { customerBO } };
            }
            else
                return new JsonResult { Data =   "Not found"  };
        }

        // POST: api/Customer
        public JsonResult Post([FromBody] CustomerBO customer)
        {
            Data.customer _customer = new Data.customer();
            _customer.custid = customer.CustId;
            _customer.custname = customer.CustName;
            _customer.creditstatus = customer.CreditStatus;
            _customer.creditlimit = customer.CreditLimit;
            _customer.creditstatus = customer.CreditStatus;
            _customer.customergroup = customer.CustomerGroup;
           Guid userId =repo.Add(_customer);
            if(userId!= null && userId != Guid.Empty)
            return new JsonResult { Data = new { status = HttpStatusCode.OK, userId = userId } };
            else
                return new JsonResult { Data = new { status = HttpStatusCode.InternalServerError, userId = "" } };
        }
        [System.Web.Http.HttpPut]
        // PUT: api/Customer/5
        public JsonResult Put(int id, [FromBody]CustomerBO customer)
        {
            Data.customer _customer = new Data.customer();
            _customer.custid = customer.CustId;
            _customer.custname = customer.CustName;
            _customer.creditstatus = customer.CreditStatus;
            _customer.creditlimit = customer.CreditLimit;
            _customer.creditstatus = customer.CreditStatus;
            _customer.customergroup = customer.CustomerGroup;
           bool result= repo.Update(_customer);
           if(result)
                return new JsonResult { Data = new { status = HttpStatusCode.OK } };
            else
                return new JsonResult { Data = new { status = HttpStatusCode.InternalServerError} };
        }

       
    }
}
