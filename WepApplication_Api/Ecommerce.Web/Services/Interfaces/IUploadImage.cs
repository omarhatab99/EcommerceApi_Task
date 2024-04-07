namespace Ecommerce.Web.Services.Interfaces
{
    public interface IUploadImage
    {
        public Task<ResultFile> UploadFile(IFormFile formFile, string folderName, bool? hasThumbnail = true);
        public void RemoveFile(string imageUrl, string? thumbUrl = null);
    }
}
