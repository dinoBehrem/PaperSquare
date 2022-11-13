using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.Infrastructure.Features.UserManagement
{
    public class UserService : IUserService
    {
        private readonly PaperSquareDbContext _paperSquareDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        DbSet<User> _users => _paperSquareDbContext.Set<User>();

        public UserService(PaperSquareDbContext paperSquareDbContext, UserManager<User> userManager, IMapper mapper)
        {
            _paperSquareDbContext = paperSquareDbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result> CreateUserAsync(UserRegistrationRequest request)
        {
            Guard.Against.Null(request, nameof(request));
            
            var user = _mapper.Map<User>(request);

            SetDefaultsForUser(user);

            if (!CheckIfPasswordsMatch(request))
            {
                return Result.Error("Password and confirm password doesn`t match!");
            }

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Error(result.Errors.Select(err => err.Description).ToArray());
            }

            result = await _userManager.AddToRoleAsync(user, Roles.User);

            if (!result.Succeeded)
            {
                await _paperSquareDbContext.Database.RollbackTransactionAsync();

                return Result.Error(result.Errors.Select(err => err.Description).ToArray());
            }

            return Result.Success();  
        }

        #region Utils

        private bool CheckIfPasswordsMatch(UserRegistrationRequest request)
        {
            return request.Password == request.ConfirmPassword;
        }

        private void SetDefaultsForUser(User user)
        {
            user.CreationDate = DateTime.UtcNow;
            user.BirthDate = DateTime.UtcNow;
        }

        #endregion Utils
    }
}
