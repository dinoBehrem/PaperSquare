using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.API.Feature.Users.V_1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user-registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody]UserRegistrationRequest registrationRequest)
        {
            var result = await _userService.CreateUserAsync(registrationRequest);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errors = result.Errors, validationErrors = result.ValidationErrors });
            }

            return CreatedAtAction(nameof(Registration), result);
        }
    }
}
