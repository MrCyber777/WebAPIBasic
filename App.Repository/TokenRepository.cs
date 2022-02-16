using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime _iJSRuntime;
        public TokenRepository(IJSRuntime ijSRuntime)
        {
            _iJSRuntime= ijSRuntime;
        }
        //public string Token { get; set; }
        public async Task SetToken(string token)
        {
            await _iJSRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
            //Token = token;  
        }
        public async Task<string?> GetToken()
        {
            var token = await _iJSRuntime.InvokeAsync<string?>("sessionStorage.getItem","token");
            return token;
            //return Token;
        }
    }
}
