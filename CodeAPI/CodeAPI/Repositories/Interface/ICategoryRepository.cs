using CodeAPI.Models.Domain;

namespace CodeAPI.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync(string? query = null,
            string? sortBy=null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);
        Task<Category?> FindByIdAsync(Guid id);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteCategory(Guid id);
        Task<int> GetCount();

    }
}
