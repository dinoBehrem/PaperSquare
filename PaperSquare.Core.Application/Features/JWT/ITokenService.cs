using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.JWT;

public interface ITokenService
{
    Task<TokenResource> BuildToken(IEnumerable<Claim> claims);
    Task<TokenResource> BuildRefreshToken(User user);
}
