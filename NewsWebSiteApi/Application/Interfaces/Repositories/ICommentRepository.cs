using NewsWebSiteApi.Domain.Entities.Comment;

namespace NewsWebSiteApi.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAll(int articleId);
    Task<Comment?> GetById(int id);
    Task<bool> Create(Comment comment);
    Task<bool> Update(Comment comment);
    Task<bool> Delete(int id);
}
