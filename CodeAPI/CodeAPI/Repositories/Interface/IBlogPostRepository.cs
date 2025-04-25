using CodeAPI.Models.Domain;

namespace CodeAPI.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost>CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GettAllAsync();
    }
}
