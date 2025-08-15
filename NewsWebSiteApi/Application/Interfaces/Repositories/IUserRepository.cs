using NewsWebSiteApi.Domain.Entities.User;

namespace NewsWebSiteApi.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);



    }
}
