using Ardalis.GuardClauses;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.Common;

public sealed class AuthResponse
{
    public TokenResource AccessToken { get; private set; }
    public TokenResource RefreshToken { get; private set; }

    private AuthResponse(TokenResource accessToken, TokenResource refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static AuthResponse Create(List<Claim>? claims, User user, TokenConfiguration tokenConfiguration)
    {
        Guard.Against.Null(claims, nameof(claims));

        var expiriation = DateTime.UtcNow.AddMinutes(5);

        var token = new JwtSecurityToken(
        issuer: null,
        audience: null,
            claims: claims,
            expires: expiriation,
            signingCredentials: tokenConfiguration.SigningCredentials);

        var accessToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiriation);

        var expirationOffset = tokenConfiguration.RefreshTokenDuration;
        var expiriationDateTime = DateTime.UtcNow.Add(expirationOffset);

        var userRefreshToken = user.AddRefreshToken(expiriationDateTime);

        var refreshToken = new TokenResource(userRefreshToken.Id, expiriationDateTime);

        var authResponse = new AuthResponse(accessToken, refreshToken);

        return authResponse;
    }
}
