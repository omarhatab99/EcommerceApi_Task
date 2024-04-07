namespace Ecommerce.Domain.Common
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
