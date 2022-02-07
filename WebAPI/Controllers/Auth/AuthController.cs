using Microsoft.AspNetCore.Mvc;
using WebAPI.Auth;

namespace WebAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<string> AuthenticateAsync(UserCredential userCredential)
        {
            var result = await Task.FromResult(_customUserManager.Authenticate(userCredential.userName,userCredential.password));// FromResult - посылает запрос и получает string
            return result;
        }
        [HttpGet]
        [Route("/verifytoken")]
        public async Task<bool> VerifyTokenAsync(string token)
        {
            var result = await Task.FromResult(_customTokenManager.VerifyToken(token));
            return result;
        }
        [HttpGet]
        [Route("/getuserinfo")]
        public async Task<string> GetUserInfoByTokenAsync(string token)
        {
            var result = await Task.FromResult(_customTokenManager.GetUserInformationByToken(token));
            return result;
        }
    }
    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }    
    }
}
