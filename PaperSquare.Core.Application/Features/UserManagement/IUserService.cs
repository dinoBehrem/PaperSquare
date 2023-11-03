using Ardalis.Result;
using PaperSquare.Domain.Entities.Identity;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Shared;

namespace PaperSquare.Infrastructure.Features.UserManagement
{
    public interface IUserService: ICommandService<UserDto, UserSearchDto, string, UserInsertDto, UserUpdateDto>
    {
    }
}
