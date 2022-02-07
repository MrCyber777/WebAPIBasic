namespace WebAPI.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {       
        private List<Token> _tokens = new();        
        public string CreateToken(string userName)
        {
           var token= new Token(userName);
           _tokens.Add(token);
            return token.TokenString;
        }
        public bool VerifyToken(string token)
        {
            var verified=_tokens.Any(x=>token is not null &&
                                        token.Contains(x.TokenString) &&
                                        x.ExpiryDate>DateTime.Now);

            return verified;
        }
        public string GetUserInformationByToken(string token)
        {
            var tokenFromDb=_tokens.FirstOrDefault(x=>token is not null && token.Contains(x.TokenString));
            if (tokenFromDb is not null)
                return tokenFromDb.UserName;

            return string.Empty;
        }
    }
}
