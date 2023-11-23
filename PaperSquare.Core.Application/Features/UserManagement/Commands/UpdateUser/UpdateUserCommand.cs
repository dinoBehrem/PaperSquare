using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.UpdateUser;

public sealed record UpdateUserCommand(string id, string firstName, string lastName, string email): IRequest<Result<UserResponse>>
{
}
