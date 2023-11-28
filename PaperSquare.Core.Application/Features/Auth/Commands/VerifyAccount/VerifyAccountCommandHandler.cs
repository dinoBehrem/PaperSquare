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

namespace PaperSquare.Core.Application.Features.Auth.Commands.VerifyAccount;

public sealed class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result<AuthResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly TokenConfiguration _tokenConfiguration;

    public VerifyAccountCommandHandler(UserManager<User> userManager, IOptions<TokenConfiguration> options)
    {
        _userManager = userManager;
        _tokenConfiguration = options.Value;
    }

    public async Task<Result<AuthResponse>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var user = await _userManager.FindByIdAsync(request.userId);

        if (user is null && !user.IsDeleted)
        {
            throw new NotFoundEntityException($"User was not found!", typeof(User));
        }

        user.VerifyAccount(request.verificationCode);

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
