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

    public void DeleteUser(User user)
    {
        user.MarkAsDeleted();
    }
}
