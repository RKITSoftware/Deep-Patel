using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using PlacementCellManagementAPI.Business_Logic;
using PlacementCellManagementAPI.Filters;
using PlacementCellManagementAPI.Handlers;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Middleware;
using System.Text;

namespace PlacementCellManagementAPI
{
    /// <summary>
    /// Startup class for configuring middleware and services.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the Startup class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            LogManager.Setup()
                .LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(configure =>
            {
                // configure.Filters.Add(typeof(ExceptionFilter));
                configure.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt Token Based Authentication",
                    In = ParameterLocation.Header,
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
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            services.AddSingleton<IExceptionLogger, BLException>();

            services.AddScoped<AuthenticationMiddleware>();

            services.AddTransient<IAdminService, BLAdmin>();
            services.AddTransient<IUserService, BLUser>();
            services.AddTransient<IStudentService, BLStudent>();
            services.AddTransient<ICompanyService, BLCompany>();
            services.AddTransient<IJobService, BLJob>();
            services.AddTransient<ITokenService, BLToken>();

            string issuer = Configuration.GetSection("Jwt:Issuer").Get<string>();
            string key = Configuration.GetSection("Jwt:Key").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                // Enable Swagger and Swagger UI in development environment.
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureExceptionHandler();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure =>
            {
                configure.MapControllers();
            });
        }
    }
}
