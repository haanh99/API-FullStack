using CodeAPI.Models.Domain;

namespace CodeAPI.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost>CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GettAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
    }
}
