using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Features.Auth.Commands.VerifyAccount;
using PaperSquare.Core.Application.Shared.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Auth.V_1;

public static class VerifyAccountEndpoint
{
    public static RouteGroupBuilder MapVerifyAccount(this RouteGroupBuilder group)
    {
        group.MapPost("/verify-account", VerifyAccount)
           .Produces<ApiSuccessResponse<AuthResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
           .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json);

        return group;
    }

    public static async Task<IResult> VerifyAccount([FromBody] VerifyAccountCommand verifyAccount, IMediator mediator, IValidator<VerifyAccountCommand> validator)
    {
        var validationResult = await validator.ValidateAsync(verifyAccount);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest();
        }

        var result = await mediator.Send(verifyAccount);

        return Results.Ok(new ApiSuccessResponse<AuthResponse>(result.Value));
    }
}
