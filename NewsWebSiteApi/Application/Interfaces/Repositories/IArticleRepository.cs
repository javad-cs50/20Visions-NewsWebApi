using NewsWebSiteApi.Domain.Entities.Article;

namespace NewsWebSiteApi.Application.Interfaces.Repositories;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAll();
    Task<Article?> GetById(int id);
    Task<IEnumerable<Article?>> GetByKeyWord (string keyWords);
    Task<IEnumerable<Article>> GetFeaturedArticles ();
    Task<List<Article?>?> GlobalSearch (string text);
    Task<List<Article>> GetByTitle (string title);
    Task<List<Article>> GetByDescription (string description);
    Task<bool> Create (Article article);
    Task<bool> Update (Article article);
    Task<bool> Delete (int id);
}
