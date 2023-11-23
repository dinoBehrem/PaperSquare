using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Shared.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Querries.GetAllUsers;

public sealed record GetAllUsersRequest : SearchRequest, IRequest<Result<IEnumerable<UserResponse>>>
{
    public string? firstName { get; init; }
    public string? lastName { get; init; }

    public GetAllUsersRequest(string? firstName, string? lastName, int? page, int? pageSize) : base(page, pageSize)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}
