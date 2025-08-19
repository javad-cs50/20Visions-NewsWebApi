namespace NewsWebSiteApi.Application.Models.Article
{
    public class ShowArticleDto
    {
       
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Discription { get; set; }
        //IsFeatured property is used to divide important news from the other news
        public bool IsFeatured { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }

    }
}
