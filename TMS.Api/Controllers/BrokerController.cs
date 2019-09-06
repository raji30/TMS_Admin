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
    [JwtAuthentication]
    public class BrokerController : ApiController
    {
        BrokerBL brokerBL = new BrokerBL();
        BrokerRepository repo = new BrokerRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public HttpResponseMessage GetbyName(string name)
        {
           
           var bo= brokerBL.GetBroker(name);
            return Request.CreateResponse(HttpStatusCode.OK, bo,
                Configuration.Formatters.JsonFormatter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBrokers")]
        [SwaggerOperation("GetBrokers")]
        public HttpResponseMessage GetBrokers()
        {
            //var boList = brokerBL.GetAll();
            //return Request.CreateResponse(HttpStatusCode.OK, boList,Configuration.Formatters.JsonFormatter);

            List < Data.broker> broker = repo.GetAll().ToList();
          
            List<BrokerBO> brokerBOList = new List<BrokerBO>();


            if (broker.Count > 0)
            {
                foreach (var driv in broker)
                {
                    BrokerBO brokerBO = new BrokerBO();
                    brokerBO.BrokerKey = driv.brokerkey;
                    brokerBO.BrokerId = driv.brokerid;
                    brokerBO.BrokerName = driv.brokername;

                    var address = new AddressRepository().GetbyId(driv.addrkey);
                    brokerBO.Address = new AddressBO()
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
                    brokerBOList.Add(brokerBO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, brokerBOList, Configuration.Formatters.JsonFormatter);
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
        [Route("CreateBroker")]
        // POST api/values
        public HttpResponseMessage Post([FromBody]BrokerBO brokerBO)
        {
            Data.broker _broker = new Data.broker();

            _broker.brokerid = brokerBO.BrokerId;
            _broker.brokername = brokerBO.BrokerName;
            _broker.status = 1;
            _broker.statusdate = DateTime.Now; 

            if (brokerBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    address1 = brokerBO.Address.Address1,
                    address2 = brokerBO.Address.Address2,
                    city = brokerBO.Address.City,
                    state = brokerBO.Address.State,
                    country = brokerBO.Address.Country,
                    zipcode = brokerBO.Address.Zip,
                    email = brokerBO.Address.Email,
                    fax = brokerBO.Address.Fax,
                    addrname = _broker.brokerid
                };
                var addrkey = new AddressRepository().Add(custaddress);
                _broker.addrkey = addrkey;
            }
            Guid brokerid = repo.Add(_broker);
            if (brokerid != null && brokerid != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, brokerid, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brokerkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBrokerByID/{brokerkey}")]
        public HttpResponseMessage GetDriverByID(string brokerkey)
        {
            Data.broker broker = repo.GetbyId(Guid.Parse(brokerkey));
            BrokerBO brokerBO = new BrokerBO();

            if (broker != null)
            {
                brokerBO.BrokerId = broker.brokerid;
                brokerBO.BrokerName = broker.brokername;
                brokerBO.BrokerKey = broker.brokerkey;
                 var address = new AddressRepository().GetbyId(broker.addrkey);
                brokerBO.Address = new AddressBO()
                {
                    AddrKey =address.addrkey,
                    Address1 = address.address1,
                    Address2 = address.address2,
                    City = address.city,
                    State = address.state,
                    Zip = address.zipcode,
                    Email = address.email,
                    Phone = address.phone,
                    Fax = address.fax
                };              
                return Request.CreateResponse(HttpStatusCode.OK, brokerBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorBO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateBroker")]
        public HttpResponseMessage Put([FromBody]BrokerBO brokerBO)
        {
            Data.broker _broker = new Data.broker();

            _broker.brokername = brokerBO.BrokerName;
            _broker.brokerid = brokerBO.BrokerId;

            if (brokerBO.Address != null)
            {
                var custaddress = new Data.address()
                {
                    addrkey=brokerBO.Address.AddrKey,
                    address1 = brokerBO.Address.Address1,
                    address2 = brokerBO.Address.Address2,
                    city = brokerBO.Address.City,
                    state = brokerBO.Address.State,
                    country = brokerBO.Address.Country,
                    zipcode = brokerBO.Address.Zip,
                    email = brokerBO.Address.Email,
                    fax = brokerBO.Address.Fax,
                    addrname = _broker.brokerid
                };
                bool updated = new AddressRepository().Update(custaddress);
            }

            bool result = repo.Update(_broker);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
