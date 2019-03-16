using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace TMS.Api.App_Start
{
    public class ResponseContentTypeOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
         //  var contentTypeAttributes = apiDescription.GetControllerAndActionAttributes<Swagger>
        }
    }
}