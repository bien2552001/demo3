using Microsoft.Extensions.DependencyInjection;

namespace DOANV1.Extensions.Service.Caching
{
    public static class CachResponse_Static
    {
        public static void ConfigureResponseCaching(this IServiceCollection services) =>

        services.AddResponseCaching();
    }
}
