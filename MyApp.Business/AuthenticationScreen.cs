using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business
{
    public class AuthenticationScreen : IAuthenticationScreen
    {
        private readonly IAuthenticationRepository _authenticatioRepository;
        public AuthenticationScreen(IAuthenticationRepository authenticationRepository)
        {
            _authenticatioRepository = authenticationRepository;
        }
        public async Task<string> LoginAsync(string userName, string password)
        {
            return await _authenticatioRepository.LoginAsync(userName, password);

        }
        public async Task<string> GetUserInfoAsync(string token)
        {
            return await _authenticatioRepository.GetUserInfoAsync(token);
        }
    }
}
