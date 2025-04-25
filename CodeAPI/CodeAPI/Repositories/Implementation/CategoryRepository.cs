using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

        public Task<Category> FindByIdAsync(Guid id)
        {
            return dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
           var selectedCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (selectedCategory != null)
            {
                dbContext.Entry(selectedCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category ?? new Category();
            }
            return null;
        }

        public async Task<Category?> DeleteCategory(Guid id)
        {
            var selectedCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (selectedCategory == null)
            {
                return null;
            }
            dbContext.Categories.Remove(selectedCategory);
            await dbContext.SaveChangesAsync();
            return selectedCategory;
        }

     
    }
}
