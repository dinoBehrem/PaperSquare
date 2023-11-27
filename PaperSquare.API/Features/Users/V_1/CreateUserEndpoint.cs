using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class CreateUserEndpoint
{
    public static RouteGroupBuilder MapCreateUser(this RouteGroupBuilder group)
    {
        group.MapPost("create", CreateUser)
           .AllowAnonymous()
           .Produces<ApiSuccessResponse<UserResponse>>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<Created<ApiSuccessResponse<UserResponse>>> CreateUser([FromBody] CreateUserCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return TypedResults.Created(new Uri("https://localhost:7242"), new ApiSuccessResponse<UserResponse>(result.Value));
    }
}
