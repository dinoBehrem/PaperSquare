using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;
using PaperSquare.Core.Application.Features.UserManagement.Commands.UpdateUser;
using PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Permissions;
using System.Net.Mime;
using PaperSquare.Core.Application.Features.UserManagement.Querries.GetAllUsers;
using PaperSquare.Core.Application.Features.UserManagement.Querries.GetUserById;

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

        group.MapGet("get-all", GetAllUsers)
            .AllowAnonymous()            
            .Produces<ApiResponse<IEnumerable<UserDto>>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);
        
        group.MapGet("get-by-id", GetUserById)
            .AllowAnonymous()            
            .Produces<ApiResponse<IEnumerable<UserDto>>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

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
        
        group.MapDelete("delete/{id}", DeleteUser)
            .RequireAuthorization(Permission.RegisteredUser, Permission.FullAccess)
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
            .Produces<ApiErrorResponse>(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json);

        return routes;
    }

    public static async Task<Ok<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers([AsParameters] GetAllUsersRequest request, IMediator mediator)
    {
        var result = await mediator.Send(request);

        return TypedResults.Ok(new ApiResponse<IEnumerable<UserDto>>(result.Value));
    }
    
    public static async Task<Ok<ApiResponse<UserDto>>> GetUserById([AsParameters] GetUserByIdRequest request, IMediator mediator)
    {
        var result = await mediator.Send(request);

        return TypedResults.Ok(new ApiResponse<UserDto>(result.Value));
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

    public static async Task<Ok<ApiResponse<UserDto>>> DeleteUser([FromBody] DeleteUserCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return TypedResults.Ok(new ApiResponse<UserDto>(result.Value));
    }
}