using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Queries.GetUserById;

public sealed record GetUserByIdRequest(string id): IRequest<Result<UserResponse>>;
