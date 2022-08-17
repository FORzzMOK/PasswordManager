using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace PasswordManager.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };

            var securityReq = new OpenApiSecurityRequirement()
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
            };

            var contact = new OpenApiContact()
            {
                Name = "Mohamad Lawand",
                Email = "hello@mohamadlawand.com",
                Url = new Uri("http://www.mohamadlawand.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "Free License",
                Url = new Uri("http://www.mohamadlawand.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Minimal API - JWT Authentication with Swagger demo",
                Description = "Implementing JWT Authentication in Minimal API",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(securityReq);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
            return app;
        }
    }
}
