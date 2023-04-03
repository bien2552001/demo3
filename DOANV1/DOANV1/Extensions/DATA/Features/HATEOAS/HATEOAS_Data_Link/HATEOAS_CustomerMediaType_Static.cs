using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DOANV1.Extensions.DATA.Features.HATEOAS.HATEOAS_Data_Link
{
    public static class HATEOAS_CustomerMediaType_Static
    {
        // Cấu hình phương tiện tùy chỉnh cho HATEOAS
        public static void HATEOAS_CustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+json");
                }
                var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+xml");
                }
            });
        }
    }
}
