using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PaperSquare.Core.Application.Features.Common;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Infrastructure.Exceptions;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenComandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly TokenConfiguration _tokenConfiguration;
    private readonly ICurrentUser _currentUser;

    public RefreshTokenComandHandler(UserManager<User> userManager, IOptions<TokenConfiguration> tokenConfiguration, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _tokenConfiguration = tokenConfiguration.Value;
        _currentUser = currentUser;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var user = await _userManager.FindByIdAsync(_currentUser.Id);

        if (user is null || user.IsDeleted)
        {
            throw new InternalServerException("Unknown error occured!");
        }

        var token = user.RefreshTokens.FirstOrDefault(rt => rt.Id == request.token);

        if (token is null || !token.IsValid)
        {
            throw new BadRequestException("You haven`t confirmed your account!");
        }

        var roles = user.Roles.Select(u => u.Role.Name).ToList();

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id)
        };

        claims.AddRange(roles.Select(role => new Claim("Role", role)));

        var authResponse = AuthResponse.Create(claims, user, _tokenConfiguration);

        token.MarkAsInvalid();

        return Result<AuthResponse>.Success(authResponse);
    }
}
