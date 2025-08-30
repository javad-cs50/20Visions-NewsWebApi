using NewsWebSiteApi.Domain.Entities.Common;

namespace NewsWebSiteApi.Domain.Entities.Category;

public class Category:BaseEntity
{
    
    public string Title { get; set; }
    public string Symbol { get; set; }
    public IList<Article.Article>? Articles { get; set; }
    
}
