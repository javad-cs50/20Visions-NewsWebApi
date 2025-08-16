namespace NewsWebSiteApi.Application.Models.Comment
{
    public class CreateCommentDto
    {
        public int NewsId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
