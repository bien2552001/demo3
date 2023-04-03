using Interface.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DOANV1.Extensions.Static.Error.GlobalErrorHandling
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,

                            Message = "Internal Server Error."

                        }.ToString());
                    }
                });
            });
        }
    }
}
