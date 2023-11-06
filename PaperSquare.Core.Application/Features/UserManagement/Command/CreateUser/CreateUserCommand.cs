using Ardalis.Result;
using MediatR;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Command.CreateUser;

public sealed record CreateUserCommand(string firstName, string lastName, string email, string username, string password, string confirmPassword): IRequest<Result<UserDto>>
{}
