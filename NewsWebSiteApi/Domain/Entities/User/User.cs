using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.Article;
namespace NewsWebSiteApi.Domain.Entities.User;


public class User:BaseEntity
{
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<Article.Article> Articles { get; set; }
}
