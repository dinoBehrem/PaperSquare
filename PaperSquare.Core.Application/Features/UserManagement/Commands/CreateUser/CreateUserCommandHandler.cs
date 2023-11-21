using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;
using System.Collections.Generic;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly PaperSquareDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(PaperSquareDbContext paperSquareDbContext, UserManager<User> userManager, IMapper mapper, ICurrentUser currentUser)
    {
        _context = paperSquareDbContext;
        _userManager = userManager;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        if (request.password != request.confirmPassword)
        {
            return Result.Error("Passwords doesn`t match!");
        }

        var user = new User(request.firstName, request.lastName, request.username, request.email);

        var userCreationResult = await _userManager.CreateAsync(user, request.password);

        if (!userCreationResult.Succeeded)
        {
            throw new IdentityResultErrorException("Failed to insert user!");
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, AppRoles.REGISTERED_USER);

        if (!addToRoleResult.Succeeded)
        {
            throw new IdentityResultErrorException("Failed to assign role!");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.SuccessWithMessage("User successfully added!");
    }
}
