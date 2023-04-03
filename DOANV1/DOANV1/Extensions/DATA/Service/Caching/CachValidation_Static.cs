using Marvin.Cache.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace DOANV1.Extensions.Service.Caching
{
    public static class CachValidation_Static
    {
     
        // VALIDATION  _____CACHING 
        public static void ValidateHttpCacheHeaders(this IServiceCollection services) => services.AddHttpCacheHeaders(

         (expirationOpt) =>
         {
             expirationOpt.MaxAge = 65;

             expirationOpt.CacheLocation = CacheLocation.Private;
         },

         (validationOpt) =>

         {
             validationOpt.MustRevalidate = true;
         });



    }
}
