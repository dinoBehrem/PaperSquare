using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Permissions;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class DeleteUserEndpoint
{
    public static RouteGroupBuilder MapDeleteUser(this RouteGroupBuilder group)
    {
        group.MapDelete("delete/{id}", DeleteUser)
            .RequireAuthorization(Permission.RegisteredUser, Permission.FullAccess)
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<Ok<ApiResponse<UserDto>>> DeleteUser([FromBody] DeleteUserCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return TypedResults.Ok(new ApiResponse<UserDto>(result.Value));
    }
}
