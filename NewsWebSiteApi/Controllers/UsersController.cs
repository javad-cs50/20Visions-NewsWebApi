using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebSiteApi.Domain.Entities.User;

namespace NewsWebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {

        }
    }
}
