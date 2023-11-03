using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Domain.Entities.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Profiles;
using System.Net;

namespace PaperSquare.UnitTests.System.Infrastrucutre.UserManagement
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        private readonly PaperSquareDbContext _dbContext;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUser> _currentUser;
        private string USER_ID = string.Empty;


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

            USER_ID = databaseContext.Users.Where(u => !u.IsDeleted && u.EmailConfirmed)
                .Select(u => u.Id)
                .FirstOrDefault();

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

        //[Fact]
        //public async void GetAll_RetriveUsers_ReturnsAllUsers()
        //{
        //    // Arrange

        //    var userSearch = new UserSearchDto()
        //    {
        //        FirstName = "First name -- 1",
        //        LastName = "Last name -- 1",
        //        Page = 1,
        //        PageSize = 2
        //    };

        //    // Act

        //    var serviceResult = await _userService.GetAll(userSearch);

        //    // Assert

        //    Assert.IsType<Result<IEnumerable<UserDto>>>(serviceResult);
        //    Assert.True(serviceResult.Value.Count() == userSearch.PageSize);
        //}

        #endregion GetAll

        #region GetById

        [Fact]
        public async void GetById_RetrieveUser_ReturnsUser()
        {
            // Act 

            var serviceResult = await _userService.GetById(USER_ID);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<UserDto>>(serviceResult);
        }

        // TO DO: Add user not found!

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
            
            _userManager.Setup(_ => _.AddToRoleAsync(It.IsAny<User>(), AppRoles.REGISTERED_USER)).ReturnsAsync(IdentityResult.Success);

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

            _userManager.Setup(_ => _.AddToRoleAsync(It.IsAny<User>(), AppRoles.REGISTERED_USER)).ReturnsAsync(IdentityResult.Failed());    

            try
            {
                // Act

                var serviceResult = await _userService.Insert(userInsertData);
            }
            catch (IdentityResultErrorException exc)
            {
                Assert.IsType<IdentityResultErrorException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.InternalServerError);
                Assert.True(exc.Message == "Failed to assign role!");
                Assert.True(exc.Messages.Count == 1);
            }
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

            try
            {
                // Act

                var serviceResult = await _userService.Insert(userInsertData);
            }
            catch (IdentityResultErrorException exc)
            {
                Assert.IsType<IdentityResultErrorException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.InternalServerError);
                Assert.True(exc.Message == "Failed to insert user!");
                Assert.True(exc.Messages.Count == 1);
            }
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

            try
            {
                // Act

                var serviceResult = await _userService.Insert(userInsertData);
            }
            catch (ErrorException exc)
            {
                Assert.IsType<ErrorException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.InternalServerError);
                Assert.Equal(passwordsDoesntMatchMessage, exc.Message);
            }
        }

        #endregion Insert

        #region Update

        [Fact]
        public async void Update_UserSuccessfullyUpdated_RetrurnsResultSuccess()
        {
            // Arrnage

            var userUpdate = new UserUpdateDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "jon_doe@mail.com"
            };

            _currentUser.Setup(_ => _.Id).Returns(USER_ID);

            // Act

            var serviceResult = await _userService.Update(USER_ID, userUpdate);

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

            try
            {
                // Act

                var serviceResult = await _userService.Update(userId, userUpdate);

            }
            catch (NotFoundEntityException exc)
            {
                // Assert

                Assert.IsType<NotFoundEntityException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.NotFound);
                Assert.True(exc.Type == typeof(User));
                Assert.True(exc.Message == "User not found!");
            }
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

            try
            {
                // Act

                var serviceResult = await _userService.Update(userId, userUpdate);

            }
            catch (UnatuhorizedAccessException exc)
            {
                // Assert

                Assert.IsType<UnatuhorizedAccessException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.Unauthorized);
                Assert.True(exc.Message == "Permission denied!");
            }

        }

        #endregion Update

        #region Delete

        [Fact]
        public async void Delete_ValidUserId_ReturnsResultSuccess()
        {
            // Arrange

            var userRoles = new List<string>() { AppRoles.REGISTERED_USER, AppRoles.ADMIN };

            _currentUser.Setup(_ => _.Id).Returns(USER_ID);

            _currentUser.Setup(_ => _.Roles).Returns(userRoles.ToArray());

            // Act

            var serviceResult = await _userService.Delete(USER_ID);

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

            try
            {
                // Act

                var serviceResult = await _userService.Delete(userId);
                                
            }
            catch (NotFoundEntityException exc)
            {
                // Assert

                Assert.IsType<NotFoundEntityException>(exc);
                Assert.True(exc.StatusCode == HttpStatusCode.NotFound);
                Assert.True(exc.Type == typeof(User));
                Assert.True(exc.Message == "User not found!");
            }
        }

        #endregion Delete
    }
}
