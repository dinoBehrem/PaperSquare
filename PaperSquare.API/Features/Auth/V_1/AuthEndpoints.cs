using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Middlewares.Exceptions;
using PaperSquare.Core.Application.Features.Auth.Commands.Login;
using PaperSquare.Core.Application.Features.Auth.Commands.RefreshToken;
using PaperSquare.Core.Application.Features.Common;
using PaperSquare.Core.Domain.Entities.Identity;
using Serilog;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1;

public static class AuthEndpoints
{
    private const string auth = "auth";
    private const string auth_path = $"api/{auth}";
    private const string auth_tag_name = "Auth endpoints";

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup(auth_path)
            .AllowAnonymous()
            .WithTags(auth_tag_name)
            .WithOpenApi();

        group.MapPost("/login", Login)
            .Produces(StatusCodes.Status200OK, typeof(AuthResponse), MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);
        
        group.MapPost("/refresh-token", RefreshToken)
            .Produces(StatusCodes.Status200OK, typeof(AuthResponse), MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return routeBuilder;
    }

    public static async Task<IResult> Login([FromBody] LoginCommand login, IMediator mediator)
    {
        var result = await mediator.Send(login);

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> RefreshToken([FromBody] RefreshTokenCommand refreshToken, IMediator mediator)
    {
        var result = await mediator.Send(refreshToken);

        return Results.Ok(result.Value);
    }
}
