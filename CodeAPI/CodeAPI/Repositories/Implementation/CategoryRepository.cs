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

        public Task<Category?> FindByIdAsync(Guid id)
        {
            return dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? query = null, string? sortBy =null, string? sortDirection = null)
        {
            //Query
            var categories = dbContext.Categories.AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(query)) 
            {
                categories = categories.Where(c => c.Name.Contains(query));
            }
            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection,"asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(c => c.Name) : categories.OrderByDescending(c => c.Name);
                }
                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(c => c.UrlHandle) : categories.OrderByDescending(c => c.UrlHandle);
                }
            }

            //Pagination

            //Return a list
            return await categories.AsNoTracking().ToListAsync();
            //Same as before, just split into two things
            //return await dbContext.Categories.ToListAsync();
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
