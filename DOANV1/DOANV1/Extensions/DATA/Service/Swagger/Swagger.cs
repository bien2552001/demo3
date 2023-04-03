using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace DOANV1.Extensions.Service.Swagger
{
    public static class Swagger
    {

        // Swagger Intergrated Authorization 
        public static void ConfigureSwagger(this IServiceCollection services)
        {


            services.AddSwaggerGen(c =>
            {

                // Cấu hình Swagger browser
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DOANV1", Version = "v1" });


                // Add Authorization__Định nghĩa bảo mật 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                //Add Authorization__Thêm yêu cầu  bảo mật 
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            //Scheme = "BearerToken",
                            Name = "",
                            //In = ParameterLocation.Header,
                        },

                        new List<string>()
                    }
                });

            });

        }










    }
}
