namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public interface IUserRepository
{
    void DeleteUser(User user);
}
