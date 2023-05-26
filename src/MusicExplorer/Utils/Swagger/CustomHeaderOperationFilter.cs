using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusicExplorer.Utils.Swagger
{
    public class CustomHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var endpoint = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (endpoint.ControllerName == "Artist" && endpoint.ActionName == "GetArtistReleases")
            {
                // Custom header parameters to the operation
                operation.Parameters ??= new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-PageNumber",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema { Type = "integer" },
                    Required = true
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-PageSize",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema { Type = "integer" },
                    Required = true
                });
            }
        }
    }
}
