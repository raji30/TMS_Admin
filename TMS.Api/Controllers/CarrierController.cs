using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.BusinessLayer;
using TMS.BusinessObjects;
using TMS.Data;
using TMS.Data.TableOperations;

namespace TMS.Api.Controllers
{
    [JwtAuthentication]
    public class CarrierController : ApiController
    {
        CarrierRepository repo = new CarrierRepository();
        CarrierDL DL = new CarrierDL();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarrier")]
        [SwaggerOperation("GetCarrier")]
        public HttpResponseMessage GetCarrier()
        {
           
            List<CarrierBO> carrierlist = DL.GetCarriers();
            //List < Data.carrier > driver =  repo.GetAll().ToList();//
            return Request.CreateResponse(HttpStatusCode.OK, carrierlist, Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrierBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCarrier")]
        // POST api/values
        public HttpResponseMessage Post([FromBody]CarrierBO carrierBO)
        {
            CarrierBO carrierData = new CarrierBO();
            carrierData = carrierBO;
            carrierData.CarrierKey = Guid.NewGuid();

            if (string.IsNullOrEmpty(carrierBO.LicensePlate ))
            {
                carrierBO.LicensePlate = string.Empty;
            }

            carrierData.Status = 1;
            carrierData.CreatedDate = DateTime.Now;
            carrierData.StatusDate = DateTime.Now;

            //Data.carrier _carrier = new Data.carrier();

            //_carrier.carrierid = carrierBO.CarrierId;
            //_carrier.carriername = carrierBO.CarrierName;
            ////_carrier.isstreamline = carrierBO.isstreamline;
            //_carrier.scaccode = carrierBO.ScacCode;
            //_carrier.createdate = DateTime.Now;
            //_carrier.status = 1;
            //_carrier.statusdate = DateTime.Now;

            if (carrierBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    address1 = carrierBO.Address.Address1,
                    address2 = carrierBO.Address.Address2,
                    city = carrierBO.Address.City,
                    state = carrierBO.Address.State,
                    country = carrierBO.Address.Country,
                    zipcode = carrierBO.Address.Zip,
                    email = carrierBO.Address.Email,
                    fax = carrierBO.Address.Fax,
                    //addrname = _carrier.carrierid
                    addrname = carrierBO.CarrierName
                };
                var addrkey = new AddressRepository().Add(custaddress);
                //_carrier.addrkey = addrkey;
                carrierData.AddrKey = addrkey;
            }
            //Guid brokerid = repo.Add(_carrier);
            CarrierDL DL = new CarrierDL();
            var carrierKey = DL.InsertCarrier(carrierData);

            if (carrierKey != null && carrierKey != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, carrierKey, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrierkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarrierByID/{carrierkey}")]
        public HttpResponseMessage GetDriverByID(string carrierkey)
        {
           // Data.carrier carrier = repo.GetbyId(Guid.Parse(carrierkey));  
            
            CarrierBO carrierBO = new CarrierBO();

            carrierBO = DL.GetCarrierbyKey(Guid.Parse(carrierkey));

            if (carrierBO != null)
            {                
                var address = new AddressRepository().GetbyId(carrierBO.AddrKey);
                carrierBO.Address = new AddressBO()
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
                return Request.CreateResponse(HttpStatusCode.OK, carrierBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brokerBO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCarrier")]
        public HttpResponseMessage Put([FromBody]CarrierBO carrierBO)
        {
            Data.carrier _carrier = new Data.carrier();

            _carrier.carrierid = carrierBO.CarrierId;
            _carrier.carriername = carrierBO.CarrierName;
            _carrier.licenseplate = carrierBO.LicensePlate;
            _carrier.licenseplateexpirydate = carrierBO.LicensePlateExpiryDate;
            _carrier.scaccode = carrierBO.ScacCode;
          
            if (carrierBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    address1 = carrierBO.Address.Address1,
                    address2 = carrierBO.Address.Address2,
                    city = carrierBO.Address.City,
                    state = carrierBO.Address.State,
                    country = carrierBO.Address.Country,
                    zipcode = carrierBO.Address.Zip,
                    email = carrierBO.Address.Email,
                    fax = carrierBO.Address.Fax,
                    addrname = _carrier.carrierid
                };
                bool updated = new AddressRepository().Update(custaddress);
            }

            bool result = repo.Update(_carrier);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}
