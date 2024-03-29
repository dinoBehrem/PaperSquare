﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Features.UserManagement.Queries.GetAllUsers;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class GetAllUsersEndpoint
{
    public static RouteGroupBuilder MapGetAllUsers(this RouteGroupBuilder group)
    {
        group.MapGet("get-all", GetAllUsers)
            .AllowAnonymous()
            .Produces<ApiSuccessResponse<List<UserResponse>>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<Ok<ApiSuccessResponse<List<UserResponse>>>> GetAllUsers([AsParameters] GetAllUsersRequest request, IMediator mediator)
    {
        var result = await mediator.Send(request);

        return TypedResults.Ok(new ApiSuccessResponse<List<UserResponse>>(result.Value));
    }
}
