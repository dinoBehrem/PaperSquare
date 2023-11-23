using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Infrastructure.Extensions;

namespace PaperSquare.Core.Application.Features.UserManagement.Querries.GetAllUsers;

public sealed class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, Result<IEnumerable<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersRequestHandler(IUserRepository userRespository)
    {
        _userRepository = userRespository;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync();
        users = ApplyFilters(users, request);

        var pagedEntities = users.Select(u => u.ToUserResponse()).ToPagedList(request.page ?? 1, request.pageSize ?? 10);

        return Result.Success(pagedEntities);
    }

    private IEnumerable<User> ApplyFilters(IEnumerable<User> query, GetAllUsersRequest search = null)
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