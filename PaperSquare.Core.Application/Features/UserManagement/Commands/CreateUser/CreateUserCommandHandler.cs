using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Application.Exceptions;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Permissions;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserResponse>>
{
    private readonly IPaperSquareDbContext _context;
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(IPaperSquareDbContext paperSquareDbContext, UserManager<User> userManager)
    {
        _context = paperSquareDbContext;
        _userManager = userManager;
    }

    public async Task<Result<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        if (request.password != request.confirmPassword)
        {
            return Result.Error("Passwords doesn`t match!");
        }

        var user = User.Create(PersonalInfo.Create(request.firstName, request.lastName, new DateTime(2000, 1, 1)), request.username, request.email);

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

        return Result.Success(user.ToUserResponse(), "User successfully added!");
    }
}
