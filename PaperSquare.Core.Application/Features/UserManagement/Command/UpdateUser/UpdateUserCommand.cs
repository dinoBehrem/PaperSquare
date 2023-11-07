using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Command.UpdateUser;

public sealed record UpdateUserCommand(string id, string firstName, string lastName, string email): IRequest<Result<UserDto>>
{
}
