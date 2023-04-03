using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace DOANV1.Extensions.Service.ActionFilters
{
    public class AsyncActionFilter : IAsyncActionFilter  // Bộ lọc này dùng riêng cho phương thức Get
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            // thực thi bất kỳ mã nào trước khi hành động được thực
            var result = await next();



        }


    }
}
