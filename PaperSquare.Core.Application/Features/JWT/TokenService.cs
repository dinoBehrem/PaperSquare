using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.JWT;

public class TokenService : ITokenService
{
    private readonly TokenConfiguration _tokenConfiguration;
    private readonly IRefreshTokenService _refreshTokenService;


    public TokenService(
        IOptions<TokenConfiguration> tokenConfiguration,
        IRefreshTokenService refreshTokenService)
    {
        _tokenConfiguration = tokenConfiguration.Value;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<TokenResource> BuildToken(IEnumerable<Claim> claims)
    {
        Guard.Against.Null(claims, nameof(claims));

        var expiriation = DateTime.UtcNow.AddMinutes(5);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiriation,
            signingCredentials: _tokenConfiguration.SigningCredentials);

        return new TokenResource(new JwtSecurityTokenHandler().WriteToken(token), expiriation);
    }

    public async Task<TokenResource> BuildRefreshToken(User user)
    {
        var expirationOffset = _tokenConfiguration.RefreshTokenDuration;
        var expiriationDateTime = DateTime.UtcNow.Add(expirationOffset);

        var refreshToken = user.AddRefreshToken(expiriationDateTime);

        await _refreshTokenService.AddRefreshToken(refreshToken);

        return new TokenResource(refreshToken.Id, expiriationDateTime);
    }
}
