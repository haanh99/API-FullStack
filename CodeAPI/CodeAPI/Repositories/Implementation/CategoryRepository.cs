using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;

namespace CodeAPI.Repositories.Implementation
{
    public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
    {

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
