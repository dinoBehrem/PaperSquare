using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Application.Mapper.UserMappings;

internal static class UserResponseMapper
{
    internal static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse(user.Id, user.UserName, user.PersonalInfo);
    }
}
