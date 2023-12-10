using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Shared.Dto;

namespace PaperSquare.Core.Application.Features.Auth.Commands.VerifyAccount;

public sealed record VerifyAccountCommand(string verificationCode, string userId) : IRequest<Result<AuthResponse>>
{
}
