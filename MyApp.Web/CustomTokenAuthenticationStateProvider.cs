using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MyApp.Web
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = "";
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);// Для какого пользователя создается правило
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");// Правило
                var principal = new ClaimsPrincipal(identity);//Внедрение правила в систему Identity

                return Task.FromResult(new AuthenticationState(principal));
            }
            else
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        }
    }
}
