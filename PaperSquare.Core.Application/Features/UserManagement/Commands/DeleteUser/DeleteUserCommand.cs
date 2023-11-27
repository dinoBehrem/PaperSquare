using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;

public sealed record DeleteUserCommand(string id): IRequest<Result<UserResponse>>
{
}
