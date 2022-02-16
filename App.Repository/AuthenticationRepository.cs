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
        public async Task<string?> GetUserInfoAsync(string? token)
        {
            var result = await _webApiExecuter.InvokePostReturnString("getuserinfo", new { token = token });
            
            if(string.IsNullOrWhiteSpace(result) || result.Equals("\"\""))
                return null;

            return result;
        }

        public async Task<string?> LoginAsync(string userName, string password)
        {
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new {userName = userName,password = password });
            await _tokenRepository.SetToken(token);

            if (string.IsNullOrWhiteSpace(token) || token.Equals("\"\""))
                return null;

            return token;
        }
    }
}
