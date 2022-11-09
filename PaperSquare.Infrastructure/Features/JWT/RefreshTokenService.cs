using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            token.IsValid = false;
            await _context.SaveChangesAsync();
        }
    }
}
