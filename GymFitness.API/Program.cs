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
using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Services;
using GymFitness.Application.Abstractions.Services;
using Swashbuckle.AspNetCore.Filters;
using Azure;
using System.Text;
using Microsoft.OpenApi.Any;



namespace GymFitness.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Xác định môi trường (Development hoặc Production)
            var env = builder.Environment.EnvironmentName;
            Console.WriteLine($"Running in {env} mode"); // In ra console để kiểm tra   

            // Load appsettings.json và appsettings.{ENV}.json
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("firebase_config.json", optional: false, reloadOnChange: true) // Firebase luôn cố định
                .AddEnvironmentVariables(); // Load thêm biến môi trường nếu có

            builder.Services.AddControllers();
            //Add repository to the container.
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();

            builder.Services.AddScoped<IStaffSpecializationRepository, StaffSpecializationRepository>();
            builder.Services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository>();
            builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
            builder.Services.AddScoped<IExerciseCategoryRepository, ExerciseCategoryRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            //builder.Services.AddScoped<IWorkoutPlanExerciseRepository, WorkoutPlanExerciseRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IUserMeasurementRepository, UserMeasurementRepository>();

            builder.Services.AddScoped<IDeviceTokenRepository, DeviceTokenRepository>();

            // Add services to the container.
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
            builder.Services.AddScoped<IStaffService, StaffService>();

            builder.Services.AddScoped<IStaffSpecializationService, StaffSpecializationService>();
            builder.Services.AddScoped<IMuscleGroupService, MuscleGroupService>();
            builder.Services.AddScoped<IExerciseCategoryService, ExerciseCategoryService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            //builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
            //builder.Services.AddScoped<IWorkoutPlanExerciseService, WorkoutPlanExerciseService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();
            builder.Services.AddSingleton<IRedisService, RedisService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IUserMeasurementService, UserMeasurementService>();
            builder.Services.AddScoped<IDeviceTokenService, DeviceTokenService>();

            builder.Services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
            builder.Services.AddHttpClient("ChatGPT");
            builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();
            builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();


            builder.Services.AddHttpClient("ChatGPT");
            builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();
            builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                                             {
                                                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                                             });






            //// ✅ Thêm DbContext
            builder.Services.AddDbContext<GymbotDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Azure Db
            // builder.Services.AddDbContext<GymbotDbContext>(options =>
            //     options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnection")));



            // other services
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(Directory.GetCurrentDirectory(), "firebase_config.json"));
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });




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
                    Description = "Nhập token mà không cần 'Bearer '",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
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

                // ✅ Hỗ trợ Upload File (fix lỗi Swagger không đọc IFormFile)


                c.OperationFilter<FileUploadOperationFilter>(); // Sử dụng custom filter này
                                                                // Cấu hình để Swagger hiển thị đúng JsonPatchDocument
                
                c.OperationFilter<JsonPatchDocumentOperationFilter>(); // Sử dụng custom filter này
            });

            // ✅ Khởi tạo Firebase Admin SDK
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("firebase_config.json")
            });

            var jwtKey = builder.Configuration["JwtSettings:Key"];
            Console.WriteLine(jwtKey);
            Console.WriteLine($"Issuer: {builder.Configuration["JwtSettings:Issuer"]}");
            Console.WriteLine($"Redis: {builder.Configuration["Redis:ConnectionString"]}");
            Console.WriteLine($"Connection Strings: {builder.Configuration["ConnectionStrings:DefaultConnection"]}");
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("JwtSettings:Key", "JWT key is not configured.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));


            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero, // Không cho phép thời gian trễ

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"❌ JWT Authentication Failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            Console.WriteLine($"⚠️ JWT Challenge: {context.Error}, {context.ErrorDescription}");
                            return Task.CompletedTask;
                        }
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


            var app = builder.Build();
            app.UseRouting();


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
            app.UseMiddleware<TokenValidationMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.MapControllers();

            // ✅ Nếu Scalar API được sử dụng
            app.MapScalarApiReference();
            app.Use(async (context, next) =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                Console.WriteLine($"🔍 Token nhận được: {token}");

                await next();
            });

            app.Run();
        }
    }
}
