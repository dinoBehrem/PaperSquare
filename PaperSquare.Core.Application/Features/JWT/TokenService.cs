using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaperSquare.Domain.Entities.Identity;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
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

            return new TokenResource()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiriation = expiriation
            };
        }

        public async Task<TokenResource> BuildRefreshToken(User user)
        {
            var expirationOffset = _tokenConfiguration.RefreshTokenDuration;
            var expirationDateTime = DateTime.UtcNow.Add(expirationOffset);

            var refreshToken = user.AddRefreshToken(expirationDateTime);

            await _refreshTokenService.AddRefreshToken(refreshToken);

            return new TokenResource
            {
                Token = refreshToken.Id,
                Expiriation = expirationDateTime,
            };
        }
    }
}
