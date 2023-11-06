using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Core.Application.Features.Auth.Commands.Login;
using PaperSquare.Core.Application.Features.Common;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using PaperSquare.Infrastructure.Features.Auth.Validators;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    #region POST

    [HttpPost("login")]
    [MapToApiVersion(ApiVersions.V_1)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        //var validator = new LoginInsertRequestValidator().Validate(request);

        //if (!validator.IsValid)
        //{
        //    return BadRequest(new { errors = validator.Errors.Select(err => err.ErrorMessage) });
        //}

        var result = await _mediator.Send(request);

        return Ok(result.Value);
    }               

    [HttpPost("refresh-token")]
    [MapToApiVersion(ApiVersions.V_1)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!IsValidToken(request.Token))
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

    #endregion POST

    #region Utils

    private bool IsValidToken(string token)
    {
        return !string.IsNullOrWhiteSpace(token);
    }

    #endregion Utils
}
