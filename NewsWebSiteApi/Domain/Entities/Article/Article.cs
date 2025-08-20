using NewsWebSiteApi.Domain.Entities.Common;

namespace NewsWebSiteApi.Domain.Entities.Article;

public class Article:BaseEntity
{
   
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public string Discription { get; set; }
    //IsFeatured property is used to divide important news from the other news
    public bool IsFeatured { get; set; } = false;
    public string KeyWord { get; set; }
    public User.User User { get; set; }
    public Category.Category Category { get; set; }
    public IList<Comment.Comment>? Comments { get; set; }

}
