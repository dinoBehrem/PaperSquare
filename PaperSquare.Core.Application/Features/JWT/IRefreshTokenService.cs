using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Core.Application.Features.JWT;

public interface IRefreshTokenService
{
    Task AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetToken(string token);
    Task MarkAsInvalid(RefreshToken token);
}
