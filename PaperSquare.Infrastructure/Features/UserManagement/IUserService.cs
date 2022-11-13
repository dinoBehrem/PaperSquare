using Ardalis.Result;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.UserManagement
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(UserRegistrationRequest request);
    }
}
