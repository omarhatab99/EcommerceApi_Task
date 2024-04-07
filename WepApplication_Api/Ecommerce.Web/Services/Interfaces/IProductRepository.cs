namespace Ecommerce.Web.Services.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Product GetByName(string name);
        public Product GetByCode(string code);
    }
}
