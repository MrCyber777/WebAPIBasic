using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Auth
{
    public class JWTTokenManager : ICustomTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private byte[] secretKey;
        public JWTTokenManager(IConfiguration configuration, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _configuration = configuration;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretKey"));
        }
        public string CreateToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor//правила для генерации JWT токена
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public string? GetUserInformationByToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var jwtToken = _jwtSecurityTokenHandler.ReadToken(token.Replace("\"", string.Empty)) as JwtSecurityToken;
            var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name");// Find unique name by token

            if (claim is not null)
                return claim.Value;

            return null;
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            SecurityToken securityToken;
            try
            {
                _jwtSecurityTokenHandler.ValidateToken(token.Replace("\"", string.Empty),
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,  //Validate client key
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),//Decription client key
                        ValidateLifetime = true,// Validate key lifetime
                        ValidateAudience = false,//Validate client application
                        ValidateIssuer = false,// Validate API Server
                        ClockSkew = TimeSpan.Zero// Stop support client
                    },
                    out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return securityToken is not null;

            //if (securityToken is not null)
            //    return true;
            //else
            //    return false;
        }
    }
}
