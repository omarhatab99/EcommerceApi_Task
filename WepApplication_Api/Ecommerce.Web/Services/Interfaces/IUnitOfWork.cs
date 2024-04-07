namespace Ecommerce.Web.Services.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public IUploadImage UploadImage { get; set; }
    }
}
