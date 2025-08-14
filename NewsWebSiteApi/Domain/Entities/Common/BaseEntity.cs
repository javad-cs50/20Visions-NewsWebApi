using NewsWebSiteApi.Domain.Enum;

namespace NewsWebSiteApi.Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public String? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public String? ModifyBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public AppAction AppAction { get; set; } = AppAction.Active;
}
