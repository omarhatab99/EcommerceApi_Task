namespace Ecommerce.Infrastracture.Persistence.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence<int>("CodeSequence")
                .StartsAt(01).IncrementsBy(1);

            new ProductEntityTypeConfigurations().Configure(modelBuilder.Entity<Product>());
            new UserEntityTypeConfigurations().Configure(modelBuilder.Entity<ApplicationUser>());
        }


        //DBSETS
        public DbSet<Product> Products { get; set; }
    }
}
