using Ardalis.Result;
using AutoMapper;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Extensions;

namespace PaperSquare.Core.Application.Features.UserManagement.Querries.GetAllUsers;

public sealed class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, Result<IEnumerable<UserDto>>>
{
    private readonly PaperSquareDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersRequestHandler(PaperSquareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _context.Users.AsQueryable();
        users = ApplyFilters(users, request);

        var pagedEntities = _mapper.Map<IEnumerable<UserDto>>(users.ToPagedList(request.page ?? 1, request.pageSize ?? 10));

        return Result.Success(pagedEntities);
    }

    private IQueryable<User> ApplyFilters(IQueryable<User> query, GetAllUsersRequest search = null)
    {
        var filteredQuery = query;

        if (!string.IsNullOrWhiteSpace(search.firstName))
        {
            filteredQuery = filteredQuery.Where(user => user.PersonalInfo.FirstName.ToLower().Contains(search.firstName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(search.lastName))
        {
            filteredQuery = filteredQuery.Where(user => user.PersonalInfo.LastName.ToLower().Contains(search.lastName.ToLower()));
        }

        return filteredQuery;
    }
}
