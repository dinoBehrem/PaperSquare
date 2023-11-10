﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Commands.UpdateUser;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Permissions;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class UpdateUserEndpoint
{
    public static RouteGroupBuilder MapUpdateUser(this RouteGroupBuilder group)
    {
        group.MapPut("update/{id}", UpdateUser)
            .RequireAuthorization(Permission.RegisteredUser)
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<Ok<ApiResponse<UserDto>>> UpdateUser([FromRoute] string id, [FromBody] UpdateUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new UpdateUserCommand(id, request.firstName, request.lastName, request.email));

        return TypedResults.Ok(new ApiResponse<UserDto>(result.Value));
    }
}

public sealed record UpdateUserRequest(string firstName, string lastName, string email);