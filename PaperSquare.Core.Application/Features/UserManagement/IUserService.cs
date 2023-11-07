using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Shared;

namespace PaperSquare.Core.Application.Features.UserManagement;

public interface IUserService : ICommandService<UserDto, UserSearchDto, string, UserInsertDto, UserUpdateDto>
{
}
