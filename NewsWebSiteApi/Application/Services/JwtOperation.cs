using Microsoft.IdentityModel.Tokens;
using NewsWebSiteApi.Application.Interfaces.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsWebSiteApi.Application.Services;

public class JwtOperation:IJwtOperation
{
    private readonly IConfiguration _configuration;
    public JwtOperation(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateTokenAsync(string userName ,int userId ,string userRole)
    {
        var claims = GetClaims(userName,userId,userRole);
        var signingCredintial = GetSigningCredentials();
        var tokenOptions = GenerateTokenOption(signingCredintial, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    private List<Claim> GetClaims(string userName ,int userId,string userRole)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, userName));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        return  claims;


    }
    private SigningCredentials GetSigningCredentials()
    {
        var secretkey = _configuration["Jwt:SecretKey"];
        var secretKeyByte = Encoding.UTF8.GetBytes(secretkey);
        var symmetricKey =new SymmetricSecurityKey(secretKeyByte);
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }
    private JwtSecurityToken GenerateTokenOption(SigningCredentials signingCredentials,List<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32 (_configuration["Jwt:ExpireMinutes"])),
            signingCredentials:signingCredentials
            );
    }
}
