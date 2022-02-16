using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.Security.Claims;

namespace MyApp.Web
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;
        public CustomTokenAuthenticationStateProvider(IAuthenticationRepository authenticationRepository,ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = await _authenticationRepository.GetUserInfoAsync(await _tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);// Для какого пользователя создается правило
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");// Правило
                var principal = new ClaimsPrincipal(identity);//Внедрение правила в систему Identity

                //return Task.FromResult(new AuthenticationState(principal));
                return new AuthenticationState(principal);
            }
            else
                //return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        }
    }
}
