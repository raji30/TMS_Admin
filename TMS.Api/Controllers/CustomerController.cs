using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class CustomerController : ApiController
    {
        CustomerDL cusObj = new CustomerDL();
        // GET: api/Customer

        [HttpGet]
        // GET: api/Customer/5
        [Route("GetCustomers")]
        public HttpResponseMessage GetCustomers()
        {
            CustomerRepository repo = new CustomerRepository();
            List<Data.customer> customer = repo.GetAll().ToList();
            List<CustomerBO> customerBOList = new List<CustomerBO>();

            if(customer.Count>0)
            {
            foreach (var cust in customer)
            {
                CustomerBO customerBO = new CustomerBO();
                customerBO.CustomerKey = cust.custkey;
                customerBO.CustId = cust.custid;
                customerBO.CustName = cust.custname;
                customerBO.CustomerGroup = cust.customergroup;
                customerBO.CreditLimit = cust.creditlimit;
                customerBO.CreditStatus = cust.creditstatus;
                var address = new AddressRepository().GetbyId(cust.addrkey);
                customerBO.Address = new AddressBO()
                {
                    Address1 = address.address1,
                    Address2 = address.address2,
                    City = address.city,
                    State = address.state,
                    Zip = address.zipcode,
                    Email = address.email,
                    Phone = address.phone,
                    Fax = address.fax
                };
                customerBOList.Add(customerBO);
            }
                return Request.CreateResponse(HttpStatusCode.OK, customerBOList, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]     
        [Route("GetCustomerByID/{custkey}")]
        public HttpResponseMessage GetCustomerByID(string custkey)
        {
            CustomerRepository repo = new CustomerRepository();
            Data.customer customer = repo.GetbyId(Guid.Parse(custkey));
            if (customer != null)
            {
                CustomerBO customerBO = new CustomerBO();
                customerBO.CustomerKey = customer.custkey;
                customerBO.CustId = customer.custid;
                customerBO.CustName = customer.custname;
                customerBO.CustomerGroup = customer.customergroup;
                customerBO.CreditLimit = customer.creditlimit;
                customerBO.CreditStatus = customer.creditstatus;
                var address = new AddressRepository().GetbyId(customer.addrkey);
                customerBO.Address = new AddressBO()
                {
                    Address1 = address.address1,
                    Address2 = address.address2,
                    City = address.city,
                    State = address.state,
                    Zip = address.zipcode,
                    Email = address.email,
                    Phone = address.phone,
                    Fax = address.fax
                };

                return Request.CreateResponse(HttpStatusCode.OK, customerBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }


        [HttpGet]
        // GET: api/Customer/5
        public HttpResponseMessage Get(string name)
        {
            CustomerRepository repo = new CustomerRepository();
            Data.customer customer= repo.GetbyField(name);
            if (customer != null)
            {
                CustomerBO customerBO = new CustomerBO();
                customerBO.CustomerKey = customer.custkey;
                customerBO.CustId = customer.custid;
                customerBO.CustName = customer.custname;
                customerBO.CustomerGroup = customer.customergroup;
                customerBO.CreditLimit = customer.creditlimit;
                customerBO.CreditStatus = customer.creditstatus;
                var address = new AddressRepository().GetbyId(customer.addrkey);
                customerBO.Address = new AddressBO()
                {
                    Address1 = address.address1,
                    Address2 = address.address2,
                    City = address.city,
                    State = address.state,
                    Zip = address.zipcode,
                    Email = address.email,
                    Phone = address.phone,
                    Fax = address.fax
                };

                return Request.CreateResponse(HttpStatusCode.OK, customerBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        // POST: api/Customer
        [Route("CreateCustomer")]
        public HttpResponseMessage Post([FromBody] CustomerBO customer)
        {
            CustomerRepository repo = new CustomerRepository();
            Data.customer _customer = new Data.customer();
            _customer.custid = customer.CustId;
            _customer.custname = customer.CustName;
            _customer.creditstatus = customer.CreditStatus;
            _customer.creditlimit = customer.CreditLimit;
            _customer.creditstatus = customer.CreditStatus;
            _customer.customergroup = customer.CustomerGroup;
            if(customer.Address != null)
            {
                var custaddress = new Data.address()
                {
                    address1 = customer.Address.Address1,
                    address2 = customer.Address.Address2,
                    city = customer.Address.City,
                    state = customer.Address.State,
                    country = customer.Address.Country,
                    zipcode = customer.Address.Zip,
                    email = customer.Address.Email,
                    fax = customer.Address.Fax,
                    addrname = customer.CustName
                };
                var addrkey = new AddressRepository().Add(custaddress);
                _customer.addrkey = addrkey;
            }
           Guid custId =repo.Add(_customer);
            if(custId != null && custId != Guid.Empty)
            return Request.CreateResponse(HttpStatusCode.OK, custId, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        // PUT: api/Customer/5
        [Route("UpdateCustomer")]
        public HttpResponseMessage Put([FromBody]CustomerBO customer)
        {
            CustomerRepository repo = new CustomerRepository();
            Data.customer _customer = new Data.customer();
            _customer.custid = customer.CustId;
            _customer.custname = customer.CustName;
            _customer.creditstatus = customer.CreditStatus;
            _customer.creditlimit = customer.CreditLimit;
            _customer.creditstatus = customer.CreditStatus;
            _customer.customergroup = customer.CustomerGroup;
            if (customer.Address != null)
            {
                var custaddress = new Data.address()
                {

                    address1 = customer.Address.Address1,
                    address2 = customer.Address.Address2,
                    city = customer.Address.City,
                    state = customer.Address.State,
                    country = customer.Address.Country,
                    zipcode = customer.Address.Zip,
                    email = customer.Address.Email,
                    fax = customer.Address.Fax,
                    addrname = customer.CustName
                };
                bool updated = new AddressRepository().Update(custaddress);
                
            }
            bool result= repo.Update(_customer);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [Route("GetCustomerCredit")]
        [SwaggerOperation("GetCustomerCredit")]
        public HttpResponseMessage GetCredit (string custKey, int amount)
        {
            CustomerDL dataaccess = new CustomerDL();
           bool result= dataaccess.GetCustomerCredit(Guid.Parse(custKey), amount);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK,"Credit Approved");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Credit Denied");

        }


        [HttpGet]
        [Route("GetCustomerMaxcount/{custname}")]
        [SwaggerOperation("GetCustomerMaxcount")]
        public HttpResponseMessage GetCustomerMaxcount(string custname)
        {
            Int64 result = cusObj.GetCustomerMaxcount(custname);
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

    }
}
