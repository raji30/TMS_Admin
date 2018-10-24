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
        public HttpResponseMessage Post([FromBody] CustomerBO customer)
        {
            Data.customer _customer = new Data.customer();
            _customer.custid = customer.CustId;
            _customer.custname = customer.CustName;
            _customer.creditstatus = customer.CreditStatus;
            _customer.creditlimit = customer.CreditLimit;
            _customer.creditstatus = customer.CreditStatus;
            _customer.customergroup = customer.CustomerGroup;
            repo.Add(_customer);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT: api/Customer/5
        private void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        private void Delete(int id)
        {
        }
    }
}
