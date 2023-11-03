using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Shared;
using PaperSquare.Domain.Entities.Identity;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Infrastructure.Features.UserManagement
{
    public class UserService : CRUDService<User, UserDto, string, UserSearchDto, UserInsertDto, UserUpdateDto>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public UserService(PaperSquareDbContext paperSquareDbContext, UserManager<User> userManager, IMapper mapper, ICurrentUser currentUser) : base(paperSquareDbContext, mapper)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public override async Task<Result<UserDto>> Insert(UserInsertDto insert)
        {
            Guard.Against.Null(insert, nameof(insert));

            if (!CheckIfPasswordsMatch(insert))
            {
                return Result.Error("Passwords doesn`t match!");
            }

            var user = new User(insert.Firstname, insert.Lastname, insert.Username, insert.Email);

            var userCreationResult = await _userManager.CreateAsync(user, insert.Password);

            if (!userCreationResult.Succeeded)
            {
                throw new IdentityResultErrorException("Failed to insert user!");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, AppRoles.REGISTERED_USER);

            if (!addToRoleResult.Succeeded)
            {
                throw new IdentityResultErrorException("Failed to assign role!");
            }

            return Result.SuccessWithMessage("User successfully added!");
        }

        public override async Task<Result<UserDto>> Update(string userId, UserUpdateDto update)
        {
            Guard.Against.Null(update, nameof(update));

            if (!HasPermissionToUpdate(userId))
            {
                throw new UnatuhorizedAccessException("Permission denied!");
            }

            var user = await _entities.FindAsync(userId);

            if (user is null)
            {
                throw new NotFoundEntityException("User not found!", typeof(User));
            }

            user.LastModifiedOnUtc = DateTime.UtcNow;
            user.LastModifiedBy = _currentUser.Id;

            _mapper.Map(update, user);

            await _dbContext.SaveChangesAsync();

            return Result.Success(_mapper.Map<UserDto>(user));
        }

        public override async Task<Result<UserDto>> Delete(string userId)
        {
            var user = await _entities.FindAsync(userId);

            if (!IsvalidUser(user))
            {
                throw new NotFoundEntityException("User not found!", typeof(User));
            }

            if (!HasPermissionToDelete())
            {
                throw new UnatuhorizedAccessException("Permission denied!");
            }

            user.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return Result.Success(_mapper.Map<UserDto>(user));
        }

        #region Utils

        private bool CheckIfPasswordsMatch(UserInsertDto request)
        {
            return request.Password == request.ConfirmPassword;
        }

        public override IQueryable<User> ApplyFilters(IQueryable<User> query, UserSearchDto search = null)
        {
            var filteredQuery = base.ApplyFilters(query, search);

            if (!string.IsNullOrWhiteSpace(search.FirstName))
            {
                filteredQuery = filteredQuery.Where(user => user.Firstname.ToLower().Contains(search.FirstName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.LastName))
            {
                filteredQuery = filteredQuery.Where(user => user.Lastname.ToLower().Contains(search.LastName.ToLower()));
            }

            return filteredQuery;
        }

        private bool HasPermissionToUpdate(string userId)
        {
            return userId == _currentUser.Id;
        }

        private bool IsvalidUser(User? user)
        {
            return user is not null;
        }

        private bool HasPermissionToDelete()
        {
            return _currentUser.Roles.Any(r => r == AppRoles.ADMIN);
        }

        #endregion Utils
    }
}
