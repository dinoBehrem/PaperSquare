using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IPaperSquareDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IPaperSquareDbContext paperSquareDbContext, UserManager<User> userManager, IMapper mapper)
    {
        _context = paperSquareDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        if (request.password != request.confirmPassword)
        {
            return Result.Error("Passwords doesn`t match!");
        }

        var user = new User(PersonalInfo.Create(request.firstName, request.lastName, new DateTime(1, 1, 2000)), request.username, request.email);

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

        return Result.Success(_mapper.Map<UserDto>(user), "User successfully added!");
    }
}
