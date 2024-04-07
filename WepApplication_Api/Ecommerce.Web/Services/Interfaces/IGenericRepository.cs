namespace Ecommerce.Web.Services.Interfaces
{
    public interface IGenericRepository<TEnitity> where TEnitity : class
    {
        public IEnumerable<TEnitity> GetAll();
        public TEnitity GetById(int id);
        public int Create(TEnitity enitity);
        public int Update(TEnitity enitity);
        public int Delete(TEnitity enitity);
    }
}
