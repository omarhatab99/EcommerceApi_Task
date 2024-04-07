namespace Ecommerce.Web.Services.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public TEntity GetById(int id)
        => _applicationDbContext.Set<TEntity>().Find(id)!;
        public IEnumerable<TEntity> GetAll()
        {
            return _applicationDbContext.Set<TEntity>().ToList();
        }
        public int Create(TEntity entity)
        {
            _applicationDbContext.Set<TEntity>().Add(entity);
            return _applicationDbContext.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            try
            {
                _applicationDbContext.Set<TEntity>().Remove(entity);
                return _applicationDbContext.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
        public int Update(TEntity entity)
        {
            try
            {
                _applicationDbContext.Set<TEntity>().Update(entity);
                return _applicationDbContext.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
    }
}
