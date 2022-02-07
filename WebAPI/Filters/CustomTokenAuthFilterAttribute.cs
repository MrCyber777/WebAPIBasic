using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Auth;

namespace WebAPI.Filters
{
    public class CustomTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICustomTokenManager _customTokenManager;        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.TOKENHEADER, out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }   
            var tokenManager=context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;
            if( tokenManager is null || !tokenManager.VerifyToken(token))
            {
               context.Result=new UnauthorizedResult();
                return ;
            }

        }
    }
}
