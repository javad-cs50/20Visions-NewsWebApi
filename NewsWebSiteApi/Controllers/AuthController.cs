using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Helper;
using NewsWebSiteApi.Application.Interfaces.Jwt;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.User;

namespace NewsWebSiteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtOperation _jwt;
    public AuthController(IUserRepository userRepository,IJwtOperation jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto loginData)
    {
        var user =await _userRepository.GetByPhoneNumberAsync(loginData.PhoneNumber);


        if (user == null)
            return NotFound();
        else if (PasswordService.VerifyPassword(loginData.Password, user.PasswordHash))
        {
            return Ok(_jwt.GenerateTokenAsync(user.PhoneNumber, user.Id, user.Role));

        }
        else
            return Unauthorized();

}
}
