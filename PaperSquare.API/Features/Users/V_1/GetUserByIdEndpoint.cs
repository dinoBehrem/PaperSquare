using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Features.UserManagement.Querries.GetUserById;
using PaperSquare.Core.Permissions;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class GetUserByIdEndpoint
{
    public static RouteGroupBuilder MapGetUserById(this RouteGroupBuilder group)
    {
        group.MapGet("get-by-id", GetUserById)
            .RequireAuthorization(Permission.RegisteredUser)
            .Produces<ApiResponse<UserResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<Ok<ApiResponse<UserResponse>>> GetUserById([AsParameters] GetUserByIdRequest request, IMediator mediator)
    {
        var result = await mediator.Send(request);

        return TypedResults.Ok(new ApiResponse<UserResponse>(result.Value));
    }
}
