using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.Auth.Commands.RefreshToken;
using PaperSquare.Core.Application.Shared.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1;

public static class RefreshTokenEndpoint
{
    public static RouteGroupBuilder MapRefreshToken(this  RouteGroupBuilder group)
    {
        group.MapPost("/refresh-token", RefreshToken)
           .Produces<AuthResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<IResult> RefreshToken([FromBody] RefreshTokenCommand refreshToken, IMediator mediator)
    {
        var result = await mediator.Send(refreshToken);

        return Results.Ok(result.Value);
    }
}
