using NewsWebSiteApi.Domain.Enum;

namespace NewsWebSiteApi.Application.Models.User
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public String? CreatedBy { get; set; }
        
        
        
        
    }
}
