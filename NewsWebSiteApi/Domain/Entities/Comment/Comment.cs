using NewsWebSiteApi.Domain.Entities.Common;

namespace NewsWebSiteApi.Domain.Entities.Comment;

public class Comment:BaseEntity
{
    public int ArticleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public Article.Article Article { get; set; }
}
