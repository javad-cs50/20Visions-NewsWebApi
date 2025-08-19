using NewsWebSiteApi.Domain.Enum;

namespace NewsWebSiteApi.Application.Models.User
{
    public class ShowUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        
    }
}
