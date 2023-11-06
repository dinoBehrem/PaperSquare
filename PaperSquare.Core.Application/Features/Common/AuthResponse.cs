using Ardalis.GuardClauses;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.Common;

public sealed class AuthResponse
{
    public TokenResource AccessToken { get; init; }
    public TokenResource RefreshToken { get; init; }

    private AuthResponse(TokenResource accessToken, TokenResource refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static AuthResponse Create(List<Claim>? claims, User user, TokenConfiguration tokenConfiguration)
    {
        // Build access token
        var expiriation = DateTime.UtcNow.AddMinutes(5);

        var token = new JwtSecurityToken(
        issuer: null,
        audience: null,
            claims: claims,
            expires: expiriation,
            signingCredentials: tokenConfiguration.SigningCredentials);

        var accessToken = new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiriation);

        // Build refresh token
        var expirationOffset = tokenConfiguration.RefreshTokenDuration;
        var expiriationDateTime = DateTime.UtcNow.Add(expirationOffset);

        var userRefreshToken = user.AddRefreshToken(expiriationDateTime);

        var refreshToken = new TokenResource(userRefreshToken.Id, expiriationDateTime);

        // Create auth response
        var authResponse = new AuthResponse(accessToken, refreshToken);

        return authResponse;
    }
}
