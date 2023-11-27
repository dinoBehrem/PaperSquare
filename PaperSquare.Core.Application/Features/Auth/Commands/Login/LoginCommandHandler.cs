using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Application.Shared.Dto;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Exceptions;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly TokenConfiguration _tokenConfiguration;

    public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<TokenConfiguration> tokenConfiguration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenConfiguration = tokenConfiguration.Value;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var user = await _userManager.FindByNameAsync(request.username);

        if (user is null && !user.IsDeleted)
        {
            throw new NotFoundEntityException($"User with username: '{request.username}' not found!", typeof(User));
        }

        if (!await _signInManager.CanSignInAsync(user))
        {
            throw new BadRequestException("You haven`t confirmed your account!");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.password, true);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Incorrect username or password!");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
        {
            new Claim(AppClaimTypes.Id, user.Id),
            new Claim(AppClaimTypes.UserName, user.UserName),
            new Claim(AppClaimTypes.Email, user.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(AppClaimTypes.Role, role)));

        AuthResponse authResponse = AuthResponse.Create(claims, user, _tokenConfiguration);

        return Result<AuthResponse>.Success(authResponse);
    }
}
