using NewsWebSiteApi.Domain.Entities.Category;

namespace NewsWebSiteApi.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<bool> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int id);
    }
}
