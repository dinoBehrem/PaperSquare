using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Querries.GetUserById;

public sealed class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetUserAsync(request.id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundEntityException($"{typeof(User).Name} not found!", typeof(User));
        }

        return Result.Success(entity.ToUserResponse());
    }
}
