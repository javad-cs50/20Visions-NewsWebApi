using NewsWebSiteApi.Domain.Entities.Common;

namespace NewsWebSiteApi.Domain.Entities.Category;

public class Category:BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Symbol { get; set; }
}
