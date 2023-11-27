using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;

public sealed record CreateUserCommand(string firstName, string lastName, string email, string username, string password, string confirmPassword): IRequest<Result<UserResponse>>
{}
