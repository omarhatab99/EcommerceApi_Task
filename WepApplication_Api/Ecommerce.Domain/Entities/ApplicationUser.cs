namespace Ecommerce.Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime LastloginTime {  get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
