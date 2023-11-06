using AutoMapper;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.Core.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserInsertDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserDto>();
    }
}
