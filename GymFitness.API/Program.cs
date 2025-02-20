using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using GymFitness.API.Services;
using GymFitness.API.Services.Abstractions;
using GymFitness.Domain.Abstractions.Services;
using GymFitness.Domain.Models;
using GymFitness.Domain.Services;
using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories;
using GymFitness.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;


namespace GymFitness.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();

            // ✅ Thêm DbContext
            builder.Services.AddDbContext<GymbotDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // other services



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

            // ✅ Khởi tạo Firebase Admin SDK
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebase_config.json")
            });

            // Cấu hình xác thực JWT
            builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/gymbot-3ddf3";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/gymbot-3ddf3",
            ValidateAudience = true,
            ValidAudience = "gymbot-3ddf3",
            ValidateLifetime = true
        };
    });

            builder.Services.AddAuthorization(options =>
            {
                // Policy cho Admin
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

                // Policy cho Staff
                options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));

                // Policy cho PT
                options.AddPolicy("PTOnly", policy => policy.RequireRole("PT"));

                // Policy cho User
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

                // Policy cho Admin và Staff (Ví dụ: cấp quyền quản lý hệ thống)
                options.AddPolicy("AdminOrStaff", policy => policy.RequireRole("Admin", "Staff"));

                // Policy cho tất cả các role (nếu bạn muốn cấp quyền truy cập rộng rãi hơn)
                options.AddPolicy("AnyAuthenticated", policy => policy.RequireAuthenticatedUser());
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
            builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();

            var app = builder.Build();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // ✅ Nếu Scalar API được sử dụng
            app.MapScalarApiReference();

            app.Run();
        }
    }
}
