using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewsWebSiteApi.Application.Interfaces.Jwt;

public interface IJwtOperation
{
    public string GenerateTokenAsync(string userName, int userId, string userRole);

}
