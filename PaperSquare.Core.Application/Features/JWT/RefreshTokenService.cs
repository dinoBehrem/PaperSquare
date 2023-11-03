using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Domain.Entities.Identity;
using PaperSquare.Data.Data;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly PaperSquareDbContext _context;

        protected DbSet<RefreshToken> RefreshTokens => _context.Set<RefreshToken>();

        public RefreshTokenService(PaperSquareDbContext context)
        {
            _context = context;
        }

        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            Guard.Against.Null(refreshToken, nameof(refreshToken));

            RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetToken(string token) => await RefreshTokens.FindAsync(token);

        public async Task MarkAsInvalid(RefreshToken token)
        {
            token.MarkAsInvalid();
            await _context.SaveChangesAsync();
        }
    }
}
