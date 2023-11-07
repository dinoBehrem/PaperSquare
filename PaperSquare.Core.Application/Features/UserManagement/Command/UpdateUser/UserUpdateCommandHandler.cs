using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Command.UpdateUser;

public sealed class UserUpdateCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly PaperSquareDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public UserUpdateCommandHandler(PaperSquareDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.id == _currentUser.Id)
        {
            throw new UnatuhorizedAccessException("Permission denied!");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundEntityException($"User with id: '{request.id}', not found!", typeof(User));
        }

        user.SetFirstname(request.firstName);
        user.SetLastname(request.lastName);
        user.SetEmail(request.email);
        
        await _context.SaveChangesAsync();

        return Result.Success(_mapper.Map<UserDto>(user));
    }
}
