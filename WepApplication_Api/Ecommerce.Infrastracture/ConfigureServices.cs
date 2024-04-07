namespace Ecommerce.Infrastracture
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastractureServices(this IServiceCollection services , IConfiguration configuration)
		{
			// Add services to the container.
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			// Add services to the container.
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseLazyLoadingProxies().UseSqlServer(connectionString));

			return services;
		}
	}
}
