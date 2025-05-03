using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlogPost =  await _context.BlogPots.Include(x=> x.Categories)
                .FirstOrDefaultAsync(x =>x.Id == blogPost.Id);
            if (existingBlogPost == null)
            {
                return null;
            }
            // Update BlogPost
            _context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            //Update Category
            existingBlogPost.Categories = blogPost.Categories;

            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _context.BlogPots.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost != null)
            {
                _context.BlogPots.Remove(existingBlogPost);
                await _context.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }
    }
}
