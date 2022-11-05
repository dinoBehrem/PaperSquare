using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.Auth.Validators;
using PaperSquare.Infrastructure.Features.JWT;
using System.Net.Mime;

namespace PaperSquare.API.Feature.Auth.V_1
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region GET

        [HttpPost("login")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginInsertRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validator = new LoginInsertRequestValidator().Validate(request);
            
            if (!validator.IsValid)
            {
                return BadRequest(new { errors = validator.Errors.Select(err => err.ErrorMessage) });
            }

            var result = await _authService.Login(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errors = result.Errors.ToList() });
            }

            return Ok(new { username = request.Username, password = request.Password });
        }

        #endregion GET

        #region POST


        #endregion POST
    }
}
