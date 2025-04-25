using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;

namespace CodeAPI.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPots.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }
    }
}
