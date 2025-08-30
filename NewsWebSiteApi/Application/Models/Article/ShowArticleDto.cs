using NewsWebSiteApi.Application.Models.Category;
using NewsWebSiteApi.Application.Models.Comment;
using NewsWebSiteApi.Application.Models.User;

namespace NewsWebSiteApi.Application.Models.Article;

public class ShowArticleDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Cover { get; set; }
    public string Discription { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    //IsFeatured property is used to divide important news from the other news
    public bool IsFeatured { get; set; }

    //from other table in the DB
    public ShowUserDto UserDto { get; set; }
    public ShowCategoryDto CategoryDto { get; set; }
    public IList<ShowCommentDto>? CommentsDto { get; set; }


}
