namespace Ecommerce.Domain.Common
{
    public class JWT
    {
        public string Issur { get; set; } = null!;
        public string Audiance { get; set; } = null!;
        public string Key { get; set; } = null!;
        public double DurationInDays { get; set; }
    }
}
