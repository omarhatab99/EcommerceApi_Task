namespace Ecommerce.Web.Core.Dtos
{
    public class ProductFormDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public double Price { get; set; }
        public int MinimumQuantity { get; set; }
        public int DiscountRate { get; set; }
        public string? Image { get; set; }
        public string? Thumb { get; set; }
        [JsonIgnore]
        public IFormFile? File { get; set; }
    }
}
