using System.Threading.Tasks;
using _19GRPADS01BNT401_Assessment.UiApi.Models;
using _19GRPADS01BNT401_Assessment.UiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace _19GRPADS01BNT401_Assessment.UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        // POST: api/users/autenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
        {
            var user =  _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}