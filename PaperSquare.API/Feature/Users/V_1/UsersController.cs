using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Feature.Users.V_1
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<Role> userManager;

        public UsersController(IUserService userService, RoleManager<Role> userManager)
        {
            _userService = userService;
            this.userManager = userManager;
        }

        #region GET
        #endregion GET

        #region POST

        [HttpPost("user-registration")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody]UserRegistrationDto registrationRequest)
        {
            var result = await _userService.CreateUserAsync(registrationRequest);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errors = result.Errors, validationErrors = result.ValidationErrors });
            }

            return CreatedAtAction(nameof(Registration), result);
        }

        [HttpGet("get-users")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }


        [HttpPost("add-role")]
        [AllowAnonymous]
        public async Task<IActionResult> AddRole()
        {
            await userManager.CreateAsync(new Role()
            {
                Name = Roles.RegisteredUser
            });

            return Ok();
        }

        #endregion POST
    }
}
