using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ITokenRepository _tokenRepository;
        public AuthenticationRepository(IWebApiExecuter webApiExecuter,ITokenRepository tokenRepository)
        {
            _webApiExecuter = webApiExecuter;  
            _tokenRepository = tokenRepository;
        }
        public async Task<string> GetUserInfoAsync(string token)
        {
            var result = await _webApiExecuter.InvokePostReturnString("getuserinfo",new { token });
            return result;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new {userName = userName,password = password });
            _tokenRepository.Token = token; 
            return token;
        }
    }
}
