using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using GymFitness.Domain.Abstractions.Services;
using GymFitness.Domain.Services;
using GymFitness.Domain.Models;

namespace GymFitness.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // ✅ Thêm Swagger với cấu hình chi tiết
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GymFitness API",
                    Version = "v1",
                    Description = "API for GymFitness application",
                    Contact = new OpenApiContact
                    {
                        Name = "Support",
                        Email = "support@gymfitness.com"
                    }
                });

                // ✅ Hỗ trợ Bearer Token (JWT)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Nhập token dạng: Bearer {your_token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ✅ CORS: Có thể cấu hình động nếu cần
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // ✅ Cấu hình OpenAI
            var openAIConfig = new OpenAIConfig
            {
                ApiKey = builder.Configuration["OpenAIKey"] ?? throw new InvalidOperationException("OpenAI API Key not found in configuration"),
                BaseUrl = builder.Configuration["OpenAIBaseUrl"] ?? throw new InvalidOperationException("OpenAI Base URL not found in configuration")
            };
            builder.Services.AddSingleton(openAIConfig);
            builder.Services.AddHttpClient("ChatGPT");
            builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();

            var app = builder.Build();

            // ✅ Luôn hiển thị Swagger (không chỉ trong Development)
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/openapi/{documentName}.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/openapi/v1.json", "GymFitness API V1");
                c.RoutePrefix = "swagger";
            });

            // ✅ Middleware
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            // ✅ Nếu Scalar API được sử dụng
            app.MapScalarApiReference();

            app.Run();
        }
    }
}
