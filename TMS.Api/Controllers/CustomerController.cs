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
    [AllowAnonymous]
   // [JwtAuthentication]
    public class CustomerController : ApiController
    {
        CustomerDL cusObj = new CustomerDL();
        // GET: api/Customer

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
        [HttpGet]
        // GET: api/Customer/5
        [Route("GetCustomers")]
        public HttpResponseMessage GetCustomers()
        {
            CustomerRepository repo = new CustomerRepository();
          //  List<Data.customer> customer = repo.GetAll().ToList();

            List<CustomerBO> customer =cusObj.GetCustomers();
            List<CustomerBO> customerBOList = new List<CustomerBO>();

            if (customer.Count > 0)
            {
                foreach (var cust in customer)
                {
                    CustomerBO customerBO = new CustomerBO();
                    customerBO.CustomerKey = cust.CustomerKey;
                    customerBO.CustId = cust.CustId;
                    customerBO.CustName = cust.CustName;
                    customerBO.CustomerGroup = cust.CustomerGroup;
                    customerBO.CreditLimit = cust.CreditLimit;
                    customerBO.CreditStatus = cust.CreditStatus;
                    customerBO.CreditCheck = cust.CreditCheck;
                    customerBO.achrequired = cust.achrequired;
                    customerBO.paymentterms = cust.paymentterms;

                   var address = new AddressDL().GetAddressByKey(cust.addrkey);
                    customerBO.Address = new AddressBO()
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


                    customerBOList.Add(customerBO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, customerBOList, Configuration.Formatters.JsonFormatter);
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
        [Route("GetCustomerByID/{custkey}")]
        public HttpResponseMessage GetCustomerByID(string custkey)
        {
            CustomerRepository repo = new CustomerRepository();
         //   Data.customer customer = repo.GetbyId(Guid.Parse(custkey));

            CustomerBO customer = cusObj.GetCustomerbykey(Guid.Parse(custkey));
            if (customer != null)
            {
                CustomerBO customerBO = new CustomerBO();
                customerBO.CustomerKey = customer.CustomerKey;
                customerBO.CustId = customer.CustId;
                customerBO.CustName = customer.CustName;
                customerBO.CustomerGroup = customer.CustomerGroup;
                customerBO.CreditLimit = customer.CreditLimit;
                customerBO.CreditStatus = customer.CreditStatus;
                customerBO.CreditCheck = customer.CreditCheck;
                customerBO.achrequired = customer.achrequired;
                customerBO.paymentterms = customer.paymentterms;
               // var address = new AddressRepository().GetbyId(customer.addrkey);
                var address = new AddressDL().GetAddressByKey(customer.addrkey);
                customerBO.Address = new AddressBO()
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
                    Phone2 =address.Phone2,
                    Email2=address.Email2
                };

                return Request.CreateResponse(HttpStatusCode.OK, customerBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        // POST: api/Customer
        [Route("CreateCustomer")]
        public HttpResponseMessage Post([FromBody] CustomerBO customer)
        {
            AddressDL addr = new AddressDL();

            CustomerRepository repo = new CustomerRepository();


            //Data.customer _customer = new Data.customer();
            //_customer.custid = customer.CustId;
            //_customer.custname = customer.CustName;
            //_customer.creditstatus = customer.CreditStatus;
            //_customer.creditlimit = customer.CreditLimit;
            //_customer.creditstatus = customer.CreditStatus;
            //_customer.customergroup = customer.CustomerGroup;           

            if (customer.Address != null)
            {
                //var custaddress = new Data.address()
                //{
                //    address1 = customer.Address.Address1,
                //    address2 = customer.Address.Address2,
                //    city = customer.Address.City,
                //    state = customer.Address.State,
                //    country = customer.Address.Country,
                //    zipcode = customer.Address.Zip,
                //    email = customer.Address.Email,
                //    fax = customer.Address.Fax,
                //    addrname = customer.CustName
                //};
                //var addrkey = new AddressRepository().Add(custaddress);

                var addrkey = new AddressDL().InsertAddress(customer.Address);
                // _customer.addrkey = addrkey;
                customer.addrkey = addrkey;
            }
            var custkey = cusObj.insertCustomer(customer);

            // Guid custkey =repo.Add(_customer);
            if (custkey != null && custkey != Guid.Empty)
            return Request.CreateResponse(HttpStatusCode.OK, custkey, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        // PUT: api/Customer/5
        [Route("UpdateCustomer")]
        public HttpResponseMessage Put([FromBody]CustomerBO customer)
        {
            CustomerRepository repo = new CustomerRepository();



            //Data.customer _customer = new Data.customer();
            //_customer.custid = customer.CustId;
            //_customer.custname = customer.CustName;
            //_customer.creditstatus = customer.CreditStatus;
            //_customer.creditlimit = customer.CreditLimit;
            //_customer.achrequired = customer.achrequired;
            //_customer.paymentterms = customer.paymentterms;
            //_customer.customergroup = customer.CustomerGroup;
            //_customer.custkey = customer.CustomerKey;
            
            if (customer.Address != null)
            {
                //var custaddress = new Data.address()
                //{
                //    addrkey = customer.Address.AddrKey,
                //    address1 = customer.Address.Address1,
                //    address2 = customer.Address.Address2,
                //    city = customer.Address.City,
                //    state = customer.Address.State,
                //    country = customer.Address.Country,
                //    zipcode = customer.Address.Zip,
                //    email = customer.Address.Email,
                //    email2 = customer.Address.Email2,
                //    fax = customer.Address.Fax,
                //    phone =customer.Address.Phone,
                //    phone2 = customer.Address.Phone2,
                //    website =customer.Address.Website,
                //    addrname = customer.CustName
                //};
                //bool updated = new AddressRepository().Update(custaddress);

                AddressDL addr = new AddressDL();
                bool updated =  addr.UpdateAddress(customer.Address);


            }
          //  bool result= repo.Update(_customer);
            bool result = cusObj.updateCustomer(customer);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="custKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custname"></param>
        /// <returns></returns>
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
