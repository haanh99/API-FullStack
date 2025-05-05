using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;

namespace CodeAPI.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _applicationDbContext;
        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            this._applicationDbContext = dbContext;
        }
        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {

            //1. Upload the Image to API/Images
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);
            //2. Update the database
            // http://api/images/something.jpg
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;
            await _applicationDbContext.BlogImages.AddAsync(blogImage);
            await _applicationDbContext.SaveChangesAsync();
            return blogImage;
        }
    }
}
