using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaperSquare.API.Features.Users.V_1;
using PaperSquare.Core.Application.Features.UserManagement;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.UnitTests.System.API.Users.V_1
{
    public class UsersControllerTest
    {
        private readonly UsersController _usersController;
        private readonly Mock<IUserService> _userService;

        public UsersControllerTest()
        {
            // Mocking dependencies
            _userService = new Mock<IUserService>();

            // SUT
            _usersController = new UsersController(_userService.Object);
        }

        #region GetAll

        [Fact]
        public async void GetAll_ValidResult_ReturnsOk()
        {
            // Arrange

            var userSearchRequest = new UserSearchDto();

            var queryResult = Result<IEnumerable<UserDto>>.Success(null);

            _userService.Setup(_ => _.GetAll(userSearchRequest)).ReturnsAsync(queryResult);

            // Act

            var endpointResult = await _usersController.GetAll(userSearchRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<OkObjectResult>(endpointResult);
        }

        #endregion GetAll

        #region GetById

        [Fact]
        public async void GetById_ValidResult_ReturnsOk()
        {
            // Arrange

            var userId = "someUserId";

            var userResult = Result<UserDto>.Success(null);

            _userService.Setup(_ => _.GetById(userId)).ReturnsAsync(userResult);

            // Act

            var endpointResult = await _usersController.GetById(userId);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<OkObjectResult>(endpointResult);
        }

        #endregion GetById

        #region Insert

        [Fact]
        public async void Insert_InsertSuccessfull_ReturnsCreatedAtAction()
        {
            // Arrange

            var userInsertRequest = new UserInsertDto();

            var userInsertResult = Result<UserDto>.Success(null);

            _userService.Setup(_ => _.Insert(userInsertRequest)).ReturnsAsync(userInsertResult);

            // Act

            var endpointResult = await _usersController.Insert(userInsertRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<CreatedAtActionResult>(endpointResult);
        }
        
        #endregion Insert

        #region Update

        [Fact]
        public async void Update_SuccessfullUpdate_ReturnsOk()
        {
            // Arrange

            var userId = "someUserId";

            var userUpdateRequest = new UserUpdateDto()
            {
                FirstName = "someFirstName",
                LastName = "someLastName",
                Email = "email@mail.com"
            };

            var userUpdateResult = Result<UserDto>.Success(null);

            _userService.Setup(_ => _.Update(userId, userUpdateRequest)).ReturnsAsync(userUpdateResult);

            // Act

            var endpointResult = await _usersController.Update(userId, userUpdateRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<OkObjectResult>(endpointResult);
        }
        
        #endregion Update

        #region Delete

        [Fact]
        public async void Delete_SuccessfullDelete_ReturnsOk()
        {
            // Arrange

            var userId = "someUserId";

            var userDeleteResult = Result<UserDto>.Success(null);

            _userService.Setup(_ => _.Delete(userId)).ReturnsAsync(userDeleteResult);

            // Act

            var endpointResult = await _usersController.Delete(userId);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<OkObjectResult>(endpointResult);
        }

        #endregion Delete
    }
}
