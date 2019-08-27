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
    public class DriverController : ApiController
    {
        DriverDL routes = new DriverDL();
        DriverRepository repo = new DriverRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDriver")]
        [SwaggerOperation("AddDriver")]
        public HttpResponseMessage Post([FromBody]DriverBO[] driverBO)
        {
            var routesdata = routes.InsertDriverDetails(driverBO);
            return Request.CreateResponse(HttpStatusCode.OK, routesdata, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDrivers")]
        [SwaggerOperation("GetDrivers")]
        public HttpResponseMessage GetDrivers()
        {
            //  IEnumerable<string> dorders = doObj.GetOrders();
            //List<DriverBO> dorder = routes.GetDrivers();
            //return Request.CreateResponse(HttpStatusCode.OK, dorder, Configuration.Formatters.JsonFormatter);

           
            List<Data.driver> driver = repo.GetAll().ToList();
            List<DriverBO> driverBOList = new List<DriverBO>();

            if (driver.Count > 0)
            {
                foreach (var driv in driver)
                {
                    DriverBO driverBO = new DriverBO();
                    driverBO.DriverKey = driv.driverkey;
                    driverBO.FirstName = driv.firstname;
                    driverBO.LastName = driv.lastname;
                    driverBO.DriverId = driv.driverid;
                    driverBO.DriversLicenseNo = driv.drivinglicenseno;
                    driverBO.LicenseExpiryDate = driv.drivinglicenseexpirydate;
                    driverBO.VendorKey = driv.vendkey;

                    var address = new AddressRepository().GetbyId(driv.addrkey);
                    driverBO.Address = new AddressBO()
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
                    driverBOList.Add(driverBO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, driverBOList, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDriverByID/{driverkey}")]
        public HttpResponseMessage GetDriverByID(string driverkey)
        {
            Data.driver driver = repo.GetbyId(Guid.Parse(driverkey));
            DriverBO driverBO = new DriverBO();

            if (driver != null)
            {
                //DriverBO driverBO = new DriverBO();
               
                    driverBO.DriverKey = driver.driverkey;
                    driverBO.FirstName = driver.firstname;
                    driverBO.LastName = driver.lastname;
                    driverBO.DriverId = driver.driverid;
                    driverBO.DriversLicenseNo = driver.drivinglicenseno;
                    driverBO.LicenseExpiryDate = driver.drivinglicenseexpirydate;
                    driverBO.VendorKey = driver.vendkey;

                    var address = new AddressRepository().GetbyId(driver.addrkey);
                    driverBO.Address = new AddressBO()
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
                    //driverBO.Add(driverBO);
               
                return Request.CreateResponse(HttpStatusCode.OK, driverBO, Configuration.Formatters.JsonFormatter);
                
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDriver")]
        public HttpResponseMessage Post([FromBody] DriverBO driverBO)
        {          
            Data.driver _driver = new Data.driver();
            
            //_driver.driverkey = driverBO.DriverKey;
            _driver.firstname = driverBO.FirstName;
            _driver.lastname = driverBO.LastName;
            _driver.driverid = driverBO.DriverId;
            _driver.drivinglicenseno = driverBO.DriversLicenseNo;
            _driver.drivinglicenseexpirydate = driverBO.LicenseExpiryDate;
            _driver.vendkey = driverBO.VendorKey;

            _driver.createdate = DateTime.Now;
            _driver.status = 1;
            _driver.statusdate = DateTime.Now;

            if (driverBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    address1 = driverBO.Address.Address1,
                    address2 = driverBO.Address.Address2,
                    city = driverBO.Address.City,
                    state = driverBO.Address.State,
                    country = driverBO.Address.Country,
                    zipcode = driverBO.Address.Zip,
                    email = driverBO.Address.Email,
                    fax = driverBO.Address.Fax,
                    addrname = _driver.driverid
                };
                var addrkey = new AddressRepository().Add(custaddress);
                _driver.addrkey = addrkey;
            }
            Guid custId = repo.Add(_driver);
            if (custId != null && custId != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, custId, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverBO"></param>
        /// <returns></returns>
        [HttpPut]      
        [Route("UpdateDriver")]
        public HttpResponseMessage Put([FromBody]DriverBO driverBO)
        {           
            Data.driver _customer = new Data.driver();          
            _customer.driverkey = driverBO.DriverKey;
            _customer.firstname = driverBO.FirstName;
            _customer.lastname = driverBO.LastName;
            _customer.driverid = driverBO.DriverId;
             _customer.drivinglicenseno = driverBO.DriversLicenseNo;
            _customer.drivinglicenseexpirydate = driverBO.LicenseExpiryDate;
             _customer.vendkey = driverBO.VendorKey;

            if (driverBO.Address != null)
            {
                var custaddress = new Data.address()
                {

                    address1 = driverBO.Address.Address1,
                    address2 = driverBO.Address.Address2,
                    city = driverBO.Address.City,
                    state = driverBO.Address.State,
                    country = driverBO.Address.Country,
                    zipcode = driverBO.Address.Zip,
                    email = driverBO.Address.Email,
                    fax = driverBO.Address.Fax,
                    addrname = _customer.firstname
                };
                bool updated = new AddressRepository().Update(custaddress);
            }


            bool result = repo.Update(_customer);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }


    }
}
