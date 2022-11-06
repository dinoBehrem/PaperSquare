using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaperSquare.Core.Models.Identity;
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

            TokenResource authResponse = new TokenResource()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiriation = expiriation
            };

            return authResponse;
        }

        public async Task<TokenResource> BuildRefreshToken(User user)
        {
            var expirationOffset = _tokenConfiguration.RefreshTokenDuration;
            var expirationDateTime = DateTime.UtcNow.Add(expirationOffset);

            var randomNumber = new byte[32];

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Created = DateTime.UtcNow,
                Expires = expirationDateTime,
            };
            
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Id = Convert.ToBase64String(randomNumber);
            }

            await _refreshTokenService.AddRefreshToken(refreshToken);

            return new TokenResource
            {
                Token = refreshToken.Id,
                Expiriation = expirationDateTime,
            };
        }
    }
}
