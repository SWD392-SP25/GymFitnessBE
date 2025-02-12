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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Configure OpenAI
            var openAIConfig = new OpenAIConfig
            {
                ApiKey = builder.Configuration["OpenAIKey"] ?? throw new InvalidOperationException("OpenAI API Key not found in configuration"),
                BaseUrl = builder.Configuration["OpenAIBaseUrl"] ?? throw new InvalidOperationException("OpenAI Base URL not found in configuration")
            };

            // Register OpenAI configuration
            builder.Services.AddSingleton(openAIConfig);

            // Register HttpClient
            builder.Services.AddHttpClient("ChatGPT", client =>
            {
                client.DefaultRequestHeaders.Add("x-foo", "true");
            });

            // Register the chat completion service
            builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "/openapi/{documentName}.json";
                });
                app.MapScalarApiReference();
            }

            //app.UseHttpsRedirection();
            //app.UseCors("AllowAll");
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
