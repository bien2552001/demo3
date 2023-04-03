using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DOANV1.Extensions.Service.RateLimiting
{
    public static class ConfigurationRateLimiting_Static
    { 
        
        // RATE LIMITTING AND THROTTLING__ cấu hình giới hạn request API (giới hạn đối với bất kì API nào, không chỉ riêng Get) 
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule> { new RateLimitRule { Endpoint = "*", Limit = 50, Period = "5m" } };

            services.Configure<IpRateLimitOptions>(opt => opt.GeneralRules = rateLimitRules);


            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        }

    }
}
