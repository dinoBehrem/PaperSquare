using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Moq;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Extensions;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Profiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.UnitTests.System.Infrastrucutre.UserManagement
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        private readonly PaperSquareDbContext _dbContext;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUser> _currentUser;

        public UserServiceTest()
        {
            // Mocking dependencies
                         
            _dbContext = GetDatabaseContext().Result;
            _userManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _mapper = CreateMapper(_mapper);
            _currentUser = new Mock<ICurrentUser>();

            // SUT

            _userService = new UserService(_dbContext, _userManager.Object, _mapper, _currentUser.Object);
        }

        #region Utils

        private async Task<PaperSquareDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PaperSquareDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new PaperSquareDbContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Users.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Users.Add(new User()
                    {
                        Id = $"user-{i}-id",
                        Firstname = $"First name -- {i}",
                        Lastname = $"Last name -- {i}",
                        Email = $"testuser{i}@example.com"
                    });

                }
            }
            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        private IMapper CreateMapper(IMapper? mapper)
        {
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new UserProfile());
                });

               mapper = mappingConfig.CreateMapper();
            }

            return mapper;
        }

        #endregion Utils

        #region GetAll

        [Fact]
        public async void GetAll_RetriveUsers_ReturnsAllUsers()
        {
            // Arrange

            var userSearch = new UserSearchDto()
            {
                FirstName = "First name -- 1",
                LastName = "Last name -- 1",
                Page = 1,
                PageSize = 2
            };

            // Act

            var serviceResult = await _userService.GetAll(userSearch);

            // Assert

            Assert.IsType<Result<IEnumerable<UserDto>>>(serviceResult);
            Assert.True(serviceResult.Value.Count() == userSearch.PageSize);
        }

        #endregion GetAll

        #region GetById

        [Fact]
        public async void GetById_RetrieveUser_ReturnsUser()
        {
            // Arrange

            var userId = Guid.NewGuid().ToString();

            // Act 

            var serviceResult = await _userService.GetById(userId);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
        }

        #endregion GetById

        #region Insert

        [Fact]
        public async void Insert_ValidInsertData_ReturnsSuccess()
        {
            // Arrange

            var userInsertData = new UserInsertDto()
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "Johnny",
                Email = "john.doe@mail.com",
                Password = "John123!",
                ConfirmPassword = "John123!",
            };

            _userManager.Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            
            _userManager.Setup(_ => _.AddToRoleAsync(It.IsAny<User>(), Roles.RegisteredUser)).ReturnsAsync(IdentityResult.Success);

            const string successMessage = "User successfully added!";

            // Act 

            var serviceResult = await _userService.Insert(userInsertData);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(serviceResult.Status == ResultStatus.Ok, successMessage);
        }

        [Fact]
        public async void Insert_InsertUserFailToAddToRole_ReturnsResultError()
        {
            // Arrange

            var userInsertData = new UserInsertDto()
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "Johnny",
                Email = "john.doe@mail.com",
                Password = "John123!",
                ConfirmPassword = "John123!",
            };

            _userManager.Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            _userManager.Setup(_ => _.AddToRoleAsync(It.IsAny<User>(), Roles.RegisteredUser)).ReturnsAsync(IdentityResult.Failed());

            // Act

            var serviceResult = await _userService.Insert(userInsertData);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        [Fact]
        public async void Insert_FailedToInsertUser_ReturnsResultError()
        {
            // Arrange 

            var userInsertData = new UserInsertDto()
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "Johnny",
                Email = "john.doe@mail.com",
                Password = "John123!",
                ConfirmPassword = "John123!",
            };

            _userManager.Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            // Act

            var serviceResult = await _userService.Insert(userInsertData);

            // Assert 

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        [Fact]
        public async void Insert_PasswordsDoesntMatch_ReturnsResultError()
        {
            // Arrange 

            var userInsertData = new UserInsertDto()
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "Johnny",
                Email = "john.doe@mail.com",
                Password = "john123!",
                ConfirmPassword = "john123",
            };

            const string passwordsDoesntMatchMessage = "Passwords doesn`t match!";

            // Act

            var serviceResult = await _userService.Insert(userInsertData);

            // Assert   

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
            Assert.Equal(passwordsDoesntMatchMessage, serviceResult.Errors.First());
        }

        #endregion Insert

        #region Update

        [Fact]
        public async void Update_UserSuccessfullyUpdated_RetrurnsResultSuccess()
        {
            // Arrnage

            var userId = "user-1-id";

            var userUpdate = new UserUpdateDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "jon_doe@mail.com"
            };

            _currentUser.Setup(_ => _.Id).Returns(userId);

            // Act

            var serviceResult = await _userService.Update(userId, userUpdate);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(serviceResult.IsSuccess);
        }

        [Fact]
        public async void Update_UserNotFound_ReturnsResultnotFound()
        {
            // Arrange

            var userId = "user-11-id";

            var userUpdate = new UserUpdateDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "jon_doe@mail.com"
            };

            _currentUser.Setup(_ => _.Id).Returns(userId);

            // Act

            var serviceResult = await _userService.Update(userId, userUpdate);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.NotFound);
        }

        [Fact]
        public async void Update_NoPermissionsToUpdate_ReturnsResultError()
        {
            // Arrange

            var userId = "user-1-id";

            var userUpdate = new UserUpdateDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "jon_doe@mail.com"
            };

            _currentUser.Setup(_ => _.Id).Returns("notfound-user-id");

            // Act

            var serviceResult = await _userService.Update(userId, userUpdate);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Unauthorized);
        }

        #endregion Update

        #region Delete

        [Fact]
        public async void Delete_ValidUserId_ReturnsResultSuccess()
        {
            // Arrange

            var userId = "user-1-id";

            var userRoles = new List<string>() { Roles.RegisteredUser, Roles.Admin };

            _currentUser.Setup(_ => _.Id).Returns(userId);

            _currentUser.Setup(_ => _.Roles).Returns(userRoles.ToArray());

            // Act

            var serviceResult = await _userService.Delete(userId);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Ok);
        }

        [Fact]
        public async void Delete_InvalidUserId_ReturnsResultNotFound()
        {
            // Arrange

            var userId = "not-found-user-id";

            // Act

            var serviceResult = await _userService.Delete(userId);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.NotFound);
        }

        #endregion Delete
    }
}
