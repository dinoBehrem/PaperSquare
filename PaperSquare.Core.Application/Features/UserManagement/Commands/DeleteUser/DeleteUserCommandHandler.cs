using Ardalis.Result;
using AutoMapper;
using MediatR;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<UserDto>>
{
    private readonly PaperSquareDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public DeleteUserCommandHandler(PaperSquareDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundEntityException("User not found!", typeof(User));
        }

        if (!_currentUser.Roles.Any(r => r == AppRoles.ADMIN) || user.Id != request.id)
        {
            throw new UnatuhorizedAccessException("Permission denied!");
        }

        user.MarkAsDeleted();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(_mapper.Map<UserDto>(user));
    }
}
