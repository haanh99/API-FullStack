using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

       
        public async Task<IEnumerable<BlogPost>> GettAllAsync()
        {
            return await _context.BlogPots.Include(x => x.Categories).ToListAsync();
        }
        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
           return await _context.BlogPots.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
