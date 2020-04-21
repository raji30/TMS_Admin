using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [AllowAnonymous]
    //[JwtAuthentication]
    public class VendorController : ApiController
    {
        VendorRepository repo = new VendorRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVendors")]
        [SwaggerOperation("GetVendors")]
        public HttpResponseMessage GetDrivers()
        {
            List<Data.vendor> vendor = repo.GetAll().ToList();
            List<VendorBO> vendorList = new List<VendorBO>();

            if (vendor.Count > 0)
            {
                foreach (var driv in vendor)
                {
                    VendorBO vendorBO = new VendorBO();
                    vendorBO.vendid = driv.vendid;
                    vendorBO.vendkey = driv.vendkey;
                    vendorBO.vendname = driv.vendname;
                    vendorBO.status = driv.status;
                    vendorBO.statusdate = driv.statusdate;

                    var address = new AddressRepository().GetbyId(driv.addrkey);
                    if (address != null)
                    {
                        vendorBO.Address = new AddressBO()
                        {
                            AddrKey = address.addrkey,
                            Address1 = address.address1,
                            Address2 = address.address2,
                            City = address.city,
                            State = address.state,
                            Zip = address.zipcode,
                            Email = address.email,
                            Phone = address.phone,
                            Fax = address.fax
                        };                       
                    }
                    vendorList.Add(vendorBO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, vendorList, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVendorByID/{vendorkey}")]
        public HttpResponseMessage GetDriverByID(string vendorkey)
        {
            Data.vendor vendor = repo.GetbyId(Guid.Parse(vendorkey));
            VendorBO vendorBO = new VendorBO();

            if (vendor != null)
            {
                vendorBO.vendid = vendor.vendid;
                vendorBO.vendkey = vendor.vendkey;
                vendorBO.vendname = vendor.vendname;
                vendorBO.status = vendor.status;
                vendorBO.statusdate = vendor.statusdate;

                var address = new AddressRepository().GetbyId(vendor.addrkey);
                if (address != null)
                {
                    vendorBO.Address = new AddressBO()
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
                        Website = address.website,
                        Country = address.country
                    };
                }
                return Request.CreateResponse(HttpStatusCode.OK, vendorBO, Configuration.Formatters.JsonFormatter);

            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateVendor")]
        public HttpResponseMessage Post([FromBody] VendorBO vendorBO)
        {
            Data.vendor _vendor = new Data.vendor();

            _vendor.vendid = vendorBO.vendid;
            _vendor.vendname = vendorBO.vendname;
            _vendor.status = 1;
            _vendor.statusdate = DateTime.Now;

            if (vendorBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    addrkey = vendorBO.Address.AddrKey,
                    address1 = vendorBO.Address.Address1,
                    address2 = vendorBO.Address.Address2,
                    city = vendorBO.Address.City,
                    state = vendorBO.Address.State,
                    country = vendorBO.Address.Country,
                    zipcode = vendorBO.Address.Zip,
                    email = vendorBO.Address.Email,
                    fax = vendorBO.Address.Fax,
                    phone = vendorBO.Address.Phone,
                    website = vendorBO.Address.Website,
                    addrname = _vendor.vendid
                    
                };
                var addrkey = new AddressRepository().Add(custaddress);
                _vendor.addrkey = addrkey;
            }
            Guid custId = repo.Add(_vendor);
            if (custId != null && custId != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, custId, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorBO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateVendor")]
        public HttpResponseMessage Put([FromBody]VendorBO vendorBO)
        {
            Data.vendor _vendor = new Data.vendor();
            _vendor.vendkey = vendorBO.vendkey;
            _vendor.vendid = vendorBO.vendid;
            _vendor.vendname = vendorBO.vendname;
            _vendor.status = 1;
            _vendor.statusdate = DateTime.Now;
            if (vendorBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    addrkey = vendorBO.Address.AddrKey,
                    address1 = vendorBO.Address.Address1,
                    address2 = vendorBO.Address.Address2,
                    city = vendorBO.Address.City,
                    state = vendorBO.Address.State,
                    country = vendorBO.Address.Country,
                    zipcode = vendorBO.Address.Zip,
                    email = vendorBO.Address.Email,
                    fax = vendorBO.Address.Fax,
                    phone= vendorBO.Address.Phone,
                    website = vendorBO.Address.Website,
                    addrname = _vendor.vendname
                };
                bool updated = new AddressRepository().Update(custaddress);
            }

            bool result = repo.Update(_vendor);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}