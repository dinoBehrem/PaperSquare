using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaperSquare.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfiguration _tokenConfiguration;

        public TokenService(IOptions<TokenConfiguration> tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration.Value;
        }

        public async Task<AuthResponse> BuildToken(IEnumerable<Claim> claims)
        {
            Guard.Against.Null(claims, nameof(claims));

            var expiriation = DateTime.UtcNow.AddHours(5);

            var token = new JwtSecurityToken(
                issuer: null, 
                audience: null, 
                claims: claims, 
                expires: expiriation, 
                signingCredentials: _tokenConfiguration.SigningCredentials);

            AuthResponse authResponse = new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiriation = expiriation
            };

            return authResponse;
        }
    }
}
