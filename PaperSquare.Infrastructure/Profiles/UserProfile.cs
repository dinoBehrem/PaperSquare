using AutoMapper;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
