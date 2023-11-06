using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.JWT;

public interface ITokenService
{
    Task<TokenResource> BuildToken(IEnumerable<Claim> claims);
    Task<TokenResource> BuildRefreshToken(User user);
}
