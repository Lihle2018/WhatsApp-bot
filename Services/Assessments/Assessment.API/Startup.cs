using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Assessments.Application;
using Assessments.Infrastructure;
using HealthChecks.UI.Client;
namespace Assessments.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Configure Services
        public void ConfigureServices(IServiceCollection services)
        {
            //Carter
            services.AddCarter();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Dependency Injection
            services.AddApplicationServices();
            services.AddInfrastructureServices(_configuration);

            // JWT Authentication
            //services.AddAuthorization();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = _configuration["Jwt:Issuer"],
            //        ValidAudience = _configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            //    };
            //});

            // Health Checks
            services.AddHealthChecks()
           .AddMongoDb(
           _configuration["DatabaseSettings:ConnectionString"],
           name: "MongoDb Health",
           failureStatus: HealthStatus.Degraded)
          .AddCheck("self", () => HealthCheckResult.Healthy());
        }

        // Configure HTTP Request Pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapCarter();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions()
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
