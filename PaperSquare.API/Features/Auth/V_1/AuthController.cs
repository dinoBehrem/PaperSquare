using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using PaperSquare.Infrastructure.Features.Auth.Validators;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region POST

        [HttpPost("login")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginInsertRequest request)
        {
            var validator = new LoginInsertRequestValidator().Validate(request);

            if (!validator.IsValid)
            {
                return BadRequest(new { errors = validator.Errors.Select(err => err.ErrorMessage) });
            }

            var result = await _authService.Login(request);

            if (!result.IsSuccess)
            {
                return NotFound(new { errors = result.Errors.ToList() });
            }

            return Ok(result.Value);
        }               

        [HttpPost("refresh-token")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new { message = "Invalid token!" });
            }

            var result = await _authService.RefreshToken(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(result.Value);
        }

        //[HttpGet("refresh-token-test")]
        //[MapToApiVersion(ApiVersions.V_1)]
        //[Produces(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize]
        //public async Task<IActionResult> RefreshTokenTest()
        //{
        //    return Ok(new { message = "Refresh token working"});
        //}

        #endregion POST
    }
}
