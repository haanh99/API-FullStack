using CodeAPI.Models.Domain;
using CodeAPI.Models.DTO;
using CodeAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository) { 
            this.imageRepository = imageRepository;
        }
        //POST: {apibaseUrl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if(ModelState.IsValid)
            {
                //File Upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                var blogImageUpload = await imageRepository.Upload(file, blogImage);

                //Convert Domain to DTO
                var respose = new BlogImageDto
                {
                    Id = blogImageUpload.Id,
                    Title = blogImageUpload.Title,
                    DateCreated = blogImageUpload.DateCreated,
                    FileExtension = blogImageUpload.FileExtension,
                    FileName = blogImageUpload.FileName,
                    Url = blogImageUpload.Url
                };

                return Ok(respose);

            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }
            if(file.Length > 1000000000)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }

    }
}
