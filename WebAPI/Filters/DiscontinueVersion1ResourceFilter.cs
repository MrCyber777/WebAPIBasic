using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//namespace WebAPI.Filters
//{
//    public class DiscontinueVersion1ResourceFilter : Attribute, IResourceFilter
//    {
//        public void OnResourceExecuted(ResourceExecutedContext context){}

//        public void OnResourceExecuting(ResourceExecutingContext context)
//        {
//            /*
//               Этот метод выполняется при получении запроса от пользователя
//                И выполняется вторым после авторизации и аутентификации
//                В цепочке фильтров запроса.
//             */
//            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
//            {
//                context.Result = new BadRequestObjectResult(new {
//                    Versioning = new[] { "This version of API has expired. Please use the latest version" }

//                });
//            }               
//        }
//    }
//}
