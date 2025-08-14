using NewsWebSiteApi.Domain.Entities.Common;

namespace NewsWebSiteApi.Domain.Entities.User;

public class User:BaseEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
