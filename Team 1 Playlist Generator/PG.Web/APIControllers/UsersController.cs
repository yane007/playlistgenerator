using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Services.Mappers;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application," + JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllRegularUsers();
            var usersViewModels = users.Select(x => x.ToViewModel());

            return Ok(users);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] LoginCredentialsModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }
    }
}