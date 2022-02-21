using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyApp.Web
{
    public class JWTTokenAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public JWTTokenAuthenticationStateProvider(IAuthenticationRepository authenticationRepository,ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var token = await _tokenRepository.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var tokenJWT = jwtSecurityTokenHandler.ReadToken(token.Replace("\"",string.Empty)) as JwtSecurityToken; 
                if(tokenJWT is not null)
                {
                    List<Claim> claims = new();
                    claims.AddRange(tokenJWT.Claims);
                    var uniqueNameClaim = tokenJWT.Claims.FirstOrDefault(x => x.Type == "unique_name");
                    var roleClaim = tokenJWT.Claims.FirstOrDefault(x => x.Type == "role");
                    if (uniqueNameClaim is not null)
                        claims.Add(new Claim(ClaimTypes.Name, uniqueNameClaim.Value));
                    if (roleClaim is not null)
                        claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));

                    var identity = new ClaimsIdentity(claims, "Custom Token Auth");
                    var principal=new ClaimsPrincipal(identity);

                    return new AuthenticationState(principal);
                }
                else
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
