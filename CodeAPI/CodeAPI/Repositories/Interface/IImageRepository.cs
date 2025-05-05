using CodeAPI.Models.Domain;

namespace CodeAPI.Repositories.Interface
{
    public interface IImageRepository
    {
         Task<BlogImage> Upload(IFormFile file, BlogImage image);
    }
}
