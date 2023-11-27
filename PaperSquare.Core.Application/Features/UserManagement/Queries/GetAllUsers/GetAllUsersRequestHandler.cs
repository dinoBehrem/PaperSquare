using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Extensions;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.Data.Extensions;

namespace PaperSquare.Core.Application.Features.UserManagement.Queries.GetAllUsers;

public sealed class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, Result<List<UserResponse>>>
{
    private readonly IPaperSquareDbContext _context;

    public GetAllUsersRequestHandler(IPaperSquareDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<UserResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .WhereIf(!string.IsNullOrWhiteSpace(request.firstName), u => u.PersonalInfo.FirstName.ToLower().Contains(request.firstName.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(request.lastName), u => u.PersonalInfo.LastName.ToLower().Contains(request.lastName.ToLower()))
            .ToPagedList(request.page ?? 1, request.pageSize ?? 10)
            .Select(u => u.ToUserResponse())
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success(users);
    }    
}