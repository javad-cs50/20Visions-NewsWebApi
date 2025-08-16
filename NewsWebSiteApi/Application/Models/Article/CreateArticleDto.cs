namespace NewsWebSiteApi.Application.Models.Article
{
    public class CreateArticleDto
    {
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Discription { get; set; }
        //IsFeatured property is used to divide important news from the other news
        public bool IsFeatured { get; set; }
        public string[]? KeyWord { get; set; }
    }
}
