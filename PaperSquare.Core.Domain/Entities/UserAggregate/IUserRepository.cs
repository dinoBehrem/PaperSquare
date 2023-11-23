namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> GetUserAsync(string id, CancellationToken cancellationToken = default);
    Task<User?> GetUserWithRefreshTokensAndRolesAsync(string id, CancellationToken cancellationToken = default);
}
