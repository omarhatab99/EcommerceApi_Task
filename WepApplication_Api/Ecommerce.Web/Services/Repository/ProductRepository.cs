namespace Ecommerce.Web.Services.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Product GetByCode(string code)
        => _applicationDbContext.Products.FirstOrDefault(x => x.Code == code)!;


        public Product GetByName(string name)
        => _applicationDbContext.Products.FirstOrDefault(x => x.Name == name)!;
    }
}
