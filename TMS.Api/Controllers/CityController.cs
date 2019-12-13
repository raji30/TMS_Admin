
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
    public class CityController : ApiController
    {
        CityDL DL = new CityDL();

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("GetCity")]
        [SwaggerOperation("GetCity")]
        public HttpResponseMessage GetCity()
        {
           // List<CityBO> 
                var citylist = DL.GetCity();             
                return Request.CreateResponse(HttpStatusCode.OK, citylist, Configuration.Formatters.JsonFormatter);           
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="city"></param>
      /// <returns></returns>
        [HttpPost]
        [Route("AddCity")]
        // POST api/values
        public HttpResponseMessage Post([FromBody]CityBO city)
        {
            CityDL DL = new CityDL();
            var cityKey = DL.InsertCity(city);

            if (cityKey != null && cityKey != Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, cityKey, Configuration.Formatters.JsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="citykey"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("GetCityByID/{citykey}")]
        public HttpResponseMessage GetDriverByID(string citykey)
        {
           CityBO cityBO = new CityBO();

            cityBO = DL.GetCitybyKey(Guid.Parse(citykey));

            if (cityBO != null)
            {               
                return Request.CreateResponse(HttpStatusCode.OK, cityBO, Configuration.Formatters.JsonFormatter);
            }
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not found", Configuration.Formatters.JsonFormatter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateCity")]
        public HttpResponseMessage Put([FromBody]CityBO city)
        {
            bool result = DL.UpdateCity(city);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}

