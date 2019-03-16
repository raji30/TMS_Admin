using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Linq;
using System.Collections.Generic;

namespace TMS.Api
{
    public class TMSOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;
            if(operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "required",
                required = true,
                type = "string",
                @default = "Bearer "
            });
        }
    }
}