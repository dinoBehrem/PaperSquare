using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<UserResponse>>
{
    private readonly IPaperSquareDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public DeleteUserCommandHandler(IPaperSquareDbContext context, IUserRepository userRepository, ICurrentUser currentUser)
    {
        _context = context;
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<UserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundEntityException("User not found!", typeof(User));
        }

        if (!_currentUser.Roles.Any(r => r == AppRoles.ADMIN) || user.Id != request.id)
        {
            throw new UnatuhorizedAccessException("Permission denied!");
        }

        _userRepository.DeleteUser(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user.ToUserResponse());
    }
}
