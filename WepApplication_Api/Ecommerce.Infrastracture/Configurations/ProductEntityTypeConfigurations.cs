namespace Ecommerce.Infrastracture.Configurations
{
    public class ProductEntityTypeConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);

            //Constraints
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Category).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Code).HasDefaultValueSql("CONCAT('P0' , NEXT VALUE FOR CodeSequence)");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            //Indexes
            builder.HasIndex(x => x.Name).HasFilter("[Name] IS NOT NULL");
            builder.HasIndex(x => x.Code).IsUnique().HasFilter("[Code] IS NOT NULL");
        }
    }
}
