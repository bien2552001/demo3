using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Interface.IService;
using System.Linq;

namespace DOANV1.Extensions.Service.ActionFilters
{
    public class ValidationFilter : IActionFilter  // =======>>>>> Bộ lọc dùng cho action Post và Put 
    {
        private readonly ILoggerService _logger;
        public ValidationFilter(ILoggerService logger)
        {
            _logger = logger;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];

            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value; // Đối số truyền vào có chứa cụm "Dto" là được chấp nhận 

            if (param == null)  // nếu tham số bất kì truyền vào = null thì báo lỗi  
            {
                _logger.LogError($" ============>>>>>>>>   Object sent from client is null. Controller: {controller}, action: {action}");

                context.Result = new BadRequestObjectResult($" ============>>>>>>>>  Object is null. Controller:{controller}, action: {action} ");

                return;
            }


            if (!context.ModelState.IsValid) // Mô hình trạng thái hợp lệ khi tất cả các thuộc tính thỏa mãn yêu cầu của thuộc tính [Require] trong lớp DTO nếu khác hoặc thiếu sẽ báo lỗi Require

            {
                _logger.LogError($" ============>>>>>>>>  Invalid model state for the object. Controller:{controller}, action: {action}");

                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }


        }



        public void OnActionExecuted(ActionExecutedContext context) { }








    }


}
