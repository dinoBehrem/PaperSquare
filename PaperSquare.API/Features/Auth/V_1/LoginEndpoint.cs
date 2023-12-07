using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.Auth.Commands.Login;
using PaperSquare.Core.Application.Shared.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1;

public static class LoginEndpoint
{
    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("/login", Login)
           .Produces<ApiSuccessResponse<AuthResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<IResult> Login([FromBody] LoginCommand login, IMediator mediator)
    {
        var result = await mediator.Send(login);

        return Results.Ok(new ApiSuccessResponse<AuthResponse>(result.Value));
    }
}
