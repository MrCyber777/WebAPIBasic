namespace WebAPI.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private readonly ICustomTokenManager _customtokenManager;
        private Dictionary<string, string> _credentials = new()
        {
            {"John","Password"},
            { "Adolf", "Password2" }         
        };
        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            _customtokenManager = customTokenManager;
        }
        public string? Authenticate(string userName, string password)
        {
            // Валидация пользователя
            if (!string.IsNullOrWhiteSpace(userName) && !_credentials[userName].Equals(password))
                return string.Empty;

            // Generate token
            var token = _customtokenManager.CreateToken(userName);
            return token;
        }
    }
}
