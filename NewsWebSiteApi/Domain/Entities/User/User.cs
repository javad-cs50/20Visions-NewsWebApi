using NewsWebSiteApi.Domain.Entities.Common;
using NewsWebSiteApi.Domain.Entities.Article;
namespace NewsWebSiteApi.Domain.Entities.User;


public class User:BaseEntity
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; }= string.Empty;
    public string Role { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IList<Article.Article> Articles { get; set; }
}
