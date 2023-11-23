using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Application.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IPaperSquareDbContext _context;

    public UserRepository(IPaperSquareDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Equals(id.ToString()) && !u.IsDeleted, cancellationToken);
    }

    public async Task<User?> GetUserWithRefreshTokensAndRolesAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Include(u => u.RefreshTokens)
                                   .Include(u => u.Roles)
                                   .Where(u => u.Equals(id.ToString()) && !u.IsDeleted)
                                   .FirstOrDefaultAsync(cancellationToken);
    }
}
