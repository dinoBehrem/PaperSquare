using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.API.Feature.Users.V_1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("user-registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody]UserRegistrationRequest registrationRequest)
        {
            return Ok();
        }
    }
}
