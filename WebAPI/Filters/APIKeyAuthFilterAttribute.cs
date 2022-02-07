using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters
{
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {              
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.CLIENTIDHEADER, out var clientId))//Значение Id клиента
                context.Result = new UnauthorizedResult();
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.APIKEYHEADER, out var clientApiKey))//Значение ключа
                context.Result = new UnauthorizedResult();

            var config=context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;//Внедрение зависимости конфигурации
            //var config2 = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var apiKey = config.GetValue<string>($"ApiKey:{clientId}");// Получение секретного ключа из appsettings.json

            if (!apiKey.Equals(clientApiKey))//Проверка совпадает ли ключ от клиента с ключом из appsettings.json
                context.Result = new UnauthorizedResult();
        }
    }
}
