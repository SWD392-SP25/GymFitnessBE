using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class JsonPatchDocumentOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Chỉ áp dụng cho HTTP PATCH
        if (!context.ApiDescription.HttpMethod.Equals("PATCH", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (operation.RequestBody?.Content?.ContainsKey("application/json-patch+json") == true)
        {
            operation.RequestBody.Content["application/json-patch+json"].Schema = new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        ["op"] = new OpenApiSchema
                        {
                            Type = "string",
                            Enum = new List<IOpenApiAny>
                            {
                                new OpenApiString("replace"),
                                new OpenApiString("add"),
                                new OpenApiString("remove")
                            }
                        },
                        ["path"] = new OpenApiSchema
                        {
                            Type = "string",
                            Example = new OpenApiString("/DtoParameter")
                        },
                        ["value"] = new OpenApiSchema
                        {
                            Type = "string",
                            Example = new OpenApiString("99.99")
                        }
                    }
                }
            };
        }
    }
}
