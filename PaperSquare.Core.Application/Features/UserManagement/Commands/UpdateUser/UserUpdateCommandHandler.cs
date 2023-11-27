using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.UpdateUser;

public sealed class UserUpdateCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
{
    private readonly IPaperSquareDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public UserUpdateCommandHandler(IPaperSquareDbContext context, IUserRepository userRepository, ICurrentUser currentUser)
    {
        _context = context;
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        if (request.id != _currentUser.Id)
        {
            throw new NotFoundException("User not found!", nameof(User));
        }

        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundEntityException($"User with id: '{request.id}', not found!", typeof(User));
        }

        user.SetPersonalInfo(request.firstName, request.lastName, DateTime.UtcNow);
        user.SetEmail(request.email);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user.ToUserResponse());
    }
}
