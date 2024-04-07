namespace Ecommerce.Web.Services.Repository
{
    public class UploadImage : IUploadImage
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public UploadImage(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        //1)allowed extension
        public IEnumerable<string> AllowedExtensions { get; set; } = new List<string>() { ".jpg", ".png", ".jpeg" };
        //2)maxsize file
        public int MaxFileSize { get; set; } = 2097152;

        public async Task<ResultFile> UploadFile(IFormFile formFile, string folderName, bool? hasThumbnail = true)
        {
            //check if allowedExtensions
            var extension = Path.GetExtension(formFile.FileName);
            if (AllowedExtensions.Contains(extension))
            {
                //check maxsize file
                if (formFile.Length < MaxFileSize)
                {
                    //image is valid
                    string imageName = $"{Guid.NewGuid()}-{formFile.FileName}";
                    string path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/{folderName}", imageName);

                    //save image to application
                    using var stream = File.Create(path);
                    await formFile.CopyToAsync(stream);
                    stream.Dispose();


                    //create thumbnail
                    if (hasThumbnail == true)
                    {

                        using var image = Image.Load(formFile.OpenReadStream());

                        var ratio = (float)image.Width / 200;
                        var height = image.Height / ratio;

                        image.Mutate(i => i.Resize(width: 200, height: (int)height));

                        var thumbPath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/{folderName}/thumb", imageName);

                        image.Save(thumbPath);
                    }
                    ;


                    return new ResultFile() { Successed = true, Url = $"/images/{folderName}/{imageName}", Thumb = $"/images/{folderName}/thumb/{imageName}" };

                }
                else
                {
                    return new ResultFile() { Successed = false, ErrorMessage = "Error, image must be less than 2 mb" };
                }
            }
            else
            {
                return new ResultFile() { Successed = false, ErrorMessage = "Error, allowed extensions is jpg , jpeg , png" };
            }
        }


        public void RemoveFile(string imageUrl, string? thumbUrl = null)
        {
            //image is valid
            if (!string.IsNullOrEmpty(imageUrl))
            {
                string _imagePath = $"{webHostEnvironment.WebRootPath}{imageUrl}";
                string _thumbePath = $"{webHostEnvironment.WebRootPath}{thumbUrl}";
                if (File.Exists(_imagePath))
                {
                    System.IO.File.Delete(_imagePath);
                }

                if (!string.IsNullOrEmpty(thumbUrl))
                {
                    if (File.Exists(_thumbePath))
                    {
                        File.Delete(_thumbePath);
                    }
                }
            }
        }

    }
}
