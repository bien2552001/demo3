//using Microsoft.AspNetCore.Mvc.Filters;

//namespace DOANV1.Extensions.Service.ActionFilters
//{
//    public class ValidateIdItemExists : IAsyncActionFilter
//    {
//        private readonly IRepositoryManager _repository;

//        private readonly ILoggerManager _logger;
//        public ValidateIdItemExists(IRepositoryManager repository, ILoggerManager logger)
//        {
//            _repository = repository;

//            _logger = logger;
//        }

//        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {

//            var action = context.RouteData.Values["action"];

//            var id = (Guid)context.ActionArguments["id"];

//            var existingItem = await _repository.GetItemAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không

//            if (existingItem == null) // Nếu không tồn tại thì trả về null 
//            {
//                _logger.LogInfo($"========>>>>>>> ____id:  {id} ______with  ACTION:( {action} )doesn't exist in the database.");

//                context.Result = new NotFoundResult();
//            }

//            else
//            {
//                context.HttpContext.Items.Add("existingItem", existingItem); // Sau khi thôi đổi dữ liệu các thuộc tính thì cập nhập lại vào cở sở dũ liệu 

//                await next();
//            }
//        }

//    }
//}
