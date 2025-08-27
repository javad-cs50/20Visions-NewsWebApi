namespace NewsWebSiteApi.Application.Models.Comment;

public class ShowCommentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }

}
