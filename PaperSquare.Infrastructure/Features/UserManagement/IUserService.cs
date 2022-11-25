using Ardalis.Result;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Shared;
using PaperSquare.Infrastructure.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.UserManagement
{
    public interface IUserService: ICommandService<UserDto, UserSearchDto, string, UserInsertDto, UserInsertDto>
    {
    }
}
