using EMS.DAL.Interfaces;
using EMS.DAL;
using EMS.BAL.Interfaces;
using EMS.BAL;
using EMS.DB.Context;
using EMS.DAL.Mapper;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Http.Features;
using EMS.API.Helpers;
using EMS.API.Controllers;

namespace EMS.API;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register mappers
        services.AddTransient<IRoleMapper, RoleMapper>();
        services.AddTransient<IEmployeeMapper, EmployeeMapper>();

        // Register DALs
        services.AddTransient<IRoleDAL, RoleDAL>();
        services.AddScoped<IDropdownDAL, DropdownDAL>();
        services.AddScoped<IEmployeeDAL, EmployeeDAL>();
        services.AddScoped<IAuthDAL, AuthDAL>();

        // Register BALs
        services.AddScoped<IDropdownBAL, DropdownBAL>();
        services.AddScoped<IEmployeeBAL, EmployeeBAL>();
        services.AddScoped<IRoleBAL, RoleBAL>();
        services.AddScoped<IAuthBAL, AuthBAL>();

        // Register Serilog ILogger
        services.AddSingleton<Serilog.ILogger>(Log.Logger);

        services.AddSingleton<ProfileImagesHelper>();

        // Register DbContext
        services.AddDbContext<EMSContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

        // Add other services
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "JWT Token Authentication API",
                Description = ".NET 8 Web API"
            });

            swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
            });
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        services.Configure<FormOptions>(options =>
        {

            options.ValueLengthLimit = int.MaxValue;

            options.MultipartBodyLengthLimit = int.MaxValue;

            options.MemoryBufferThreshold = int.MaxValue;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            };
        })
        .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"), "AzureAd");

        services.AddMvc();
        services.AddRazorPages();
    }
}