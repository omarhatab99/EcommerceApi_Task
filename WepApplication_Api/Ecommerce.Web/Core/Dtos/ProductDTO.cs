namespace Ecommerce.Web.Core.Dtos
{
    public class ProductDTO:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? Code { get; set; }
        public double Price { get; set; }
        public int MinimumQuantity { get; set; }
        public int DiscountRate { get; set; }
        public string? Image { get; set; }
        public string? Thumb { get; set; }
    }
}
