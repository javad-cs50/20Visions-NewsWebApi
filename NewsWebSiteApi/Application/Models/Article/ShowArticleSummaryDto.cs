namespace NewsWebSiteApi.Application.Models.Article;

public class ShowArticleSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Cover { get; set;}
    public string SummaryDescription { get; set; }
    public String? CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public bool IsFeatured { get; set; }

}
