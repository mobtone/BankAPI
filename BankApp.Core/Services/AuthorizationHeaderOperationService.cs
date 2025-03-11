using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Services
{
    public class AuthorizationHeaderOperationService : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Kontrollera om endpointen har attributet [Authorize]
            var hasAuthorizeAttribute = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .Any(attr => attr.GetType().Name == "AuthorizeAttribute");

            if (hasAuthorizeAttribute)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                // Lägg till Authorization-headern
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Ange 'Bearer' följt av ett mellanslag och din token.",
                    Required = true, // Gör den obligatorisk
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...")
                    }
                });
            }
        }
    }
}
