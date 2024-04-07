namespace Ecommerce.Infrastracture.Configurations
{
    public class UserEntityTypeConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            //Indexes
            builder.HasIndex(x => x.UserName).IsUnique().HasFilter("[UserName] IS NOT NULL");
            builder.HasIndex(x => x.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
        }
    }
}
