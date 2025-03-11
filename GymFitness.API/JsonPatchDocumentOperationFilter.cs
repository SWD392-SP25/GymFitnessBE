using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.JsonPatch;

public class JsonPatchDocumentOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetParameters().Any(p => p.ParameterType == typeof(JsonPatchDocument)))
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json-patch+json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema { Type = "object" }
                    }
                }
            };
        }
    }
}
