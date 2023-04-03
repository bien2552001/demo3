
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DOANV1.Extensions.Static
{
    public static class StaticFile
    {
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        });




        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

    }
}
