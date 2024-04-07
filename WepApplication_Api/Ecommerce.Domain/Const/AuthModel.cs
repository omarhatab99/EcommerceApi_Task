namespace Ecommerce.Domain.Const
{
    public class AuthModel
    {
        public List<string> Messages { get; set; } = new List<string>();
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = null!;
        public DateTime? ExpiresOn { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
