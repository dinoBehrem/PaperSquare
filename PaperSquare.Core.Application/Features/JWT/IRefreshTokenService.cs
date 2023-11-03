using PaperSquare.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public interface IRefreshTokenService
    {
        Task AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetToken(string token);
        Task MarkAsInvalid(RefreshToken token);
    }
}
