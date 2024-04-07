namespace Ecommerce.Web.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public IUploadImage UploadImage { get; set; }

        public UnitOfWork(IProductRepository productRepository, IAuthRepository authRepository, IUploadImage uploadImage)
        {
            ProductRepository = productRepository;
            AuthRepository = authRepository;
            UploadImage = uploadImage;
        }

    }
}
