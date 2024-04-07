namespace Ecommerce.Domain.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = null!;
        public DateTime ExpirationOn {  get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationOn;
        public DateTime CreatedOn {  get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
