using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Application.Helper;
using NewsWebSiteApi.Application.Interfaces.Repositories;
using NewsWebSiteApi.Application.Models.User;
using NewsWebSiteApi.Domain.Entities.User;

namespace NewsWebSiteApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UsersController> _logger;
    private readonly IConfiguration _configuration;

    public UsersController(IUserRepository categoryRepository, ILogger<UsersController> logger, IConfiguration configuration)
    {
        _userRepository = categoryRepository;
        _logger = logger;
        _configuration = configuration;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShowUserDto>>> GetAllUsers()
    {
        var users =await _userRepository.GetAllAsync();
        if(users == null)
            return NotFound();

        var userDtos = users.Select(u => new ShowUserDto
        {
            Id=u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName

        });
        return Ok(userDtos);
    }
    [HttpGet("{userId}")]
    public async Task<ActionResult<ShowUserDto>> GetUserById(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return NotFound();
        var userDto = new ShowUserDto 
        { 
            Id=user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,

        };
        return Ok(userDto);
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<bool>> CreateUser([FromBody]CreateUserDto req) 
    {
        var user = new User
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            PhoneNumber = req.PhoneNumber,
            CreatedDate = DateTime.Now,
            Role = "User",
            PasswordHash = PasswordService.HashPassword((req.PlainPassword))

        };
        var isCreated = await _userRepository.CreateAsync(user);

        if (isCreated == false) 
            return BadRequest(isCreated);
        else
            return Ok(isCreated);
    }
    [HttpPut("{userId}")]
    public async Task<ActionResult<bool>> UpdateUser(int userId, [FromBody] CreateUserDto req) 
    {
        var user =await _userRepository.GetByIdAsync(userId);
        if(user == null) 
            return NotFound();
        else
        {
            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.PhoneNumber = req.PhoneNumber;
            user.ModifiedDate = DateTime.Now;
        }

        var isUpdated = await _userRepository.UpdateAsync(user);
        if (isUpdated == false)
            return BadRequest(isUpdated);
        else
            return Ok(isUpdated);

    }
    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> DeleteUser(int userId)    
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return NotFound();
        
        var isDeleted =await _userRepository.DeleteAsync(userId);
        if (isDeleted == null)
            return BadRequest(isDeleted);
        else
            return Ok(isDeleted);
        

    }
    

    
}
