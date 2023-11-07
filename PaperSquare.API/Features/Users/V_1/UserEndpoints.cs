using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Command.CreateUser;
using PaperSquare.Core.Application.Features.UserManagement.Command.UpdateUser;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Permissions;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1;

public static class UserEndpoints
{
    private const string user = "user";
    private const string user_path = $"api/{user}";
    private const string user_tag_name = "User endpoints";

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(user_path)
            .WithTags(user_tag_name)            
            .WithOpenApi();

        group.MapPost("create", CreateUser)
            .AllowAnonymous()
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);
        
        group.MapPut("update/{id}", UpdateUser)
            .RequireAuthorization(Permission.RegisteredUser)
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return routes;
    }

    public static async Task<Created<ApiResponse<UserDto>>> CreateUser([FromBody] CreateUserCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return TypedResults.Created("", new ApiResponse<UserDto>(result.Value));
    }

    public static async Task<Ok<ApiResponse<UserDto>>> UpdateUser(string id, [FromBody] UpdateUserCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return TypedResults.Ok(new ApiResponse<UserDto>(result.Value));
    }
}
