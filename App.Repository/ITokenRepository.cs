namespace MyApp.Repository
{
    public interface ITokenRepository
    {
        //string Token { get; set; }

        Task<string?> GetToken();
        Task SetToken(string token);
    }
}