using Microsoft.AspNetCore.Mvc;
using WebAPI.Auth;

namespace WebAPI.Controllers.Auth
{
    [ApiController]   
    public class AuthController:ControllerBase
    {
        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(ICustomUserManager customUserManager,ICustomTokenManager customTokenManager)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
        }
        [HttpPost]
        [Route("/authenticate")]
        public  Task<string?> AuthenticateAsync(UserCredential userCredential)
        {
            var result = Task.FromResult(_customUserManager.Authenticate(userCredential.userName,userCredential.password));// FromResult - посылает запрос и получает string
            return result;
        }
        [HttpGet]
        [Route("/verifytoken")]
        public  Task<bool> VerifyTokenAsync(Token token)
        {
            var result =  Task.FromResult(_customTokenManager.VerifyToken(token.token));
            return result;
        }
        [HttpPost]
        [Route("/getuserinfo")]
        public Task<string?> GetUserInfoByTokenAsync( Token token)
        {
            var result = Task.FromResult(_customTokenManager.GetUserInformationByToken(token.token));
            return result;
        }
    }
    public class Token
    {
        public string? token { get; set; }
    }
    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }    
    }
}
