namespace Ecommerce.Web.Services.Interfaces
{
    public interface IAuthRepository
    {
        public Task<AuthModel> RegisterAsync(RegisterUserDTO model);
        public Task<AuthModel> GetTokenAsync(LoginUserDTO model);
        public Task<AuthModel> RefreshTokenAsync(string token);
        public Task<bool> RevokeTokenAsync(string token);
    }
}
