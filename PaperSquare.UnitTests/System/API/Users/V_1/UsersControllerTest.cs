using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaperSquare.API.Features.Users.V_1;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Fact]
        public async void GetAll_InvalidResult_ReturnsBadRequset()
        {
            // Arrange

            var userSearchRequest = new UserSearchDto();

            var queryResult = Result<IEnumerable<UserDto>>.Error("Error while fetching data!");

            _userService.Setup(_ => _.GetAll(userSearchRequest)).ReturnsAsync(queryResult);

            // Act

            var endpointResult = await _usersController.GetAll(userSearchRequest);

            // Assert 

            Assert.NotNull(endpointResult);
            Assert.IsType<BadRequestObjectResult>(endpointResult);
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

        [Fact]
        public async void GetById_InvalidResult_ResturnsBadRequest()
        {
            // Arrange

            var userId = "someUserId";

            var userResult = Result<UserDto>.Error(null);

            _userService.Setup(_ => _.GetById(userId)).ReturnsAsync(userResult);

            // Act

            var endpointResult = await _usersController.GetById(userId);   
            
            // Assert

            Assert.NotNull(endpointResult); 
            Assert.IsType<BadRequestObjectResult>(endpointResult);
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
        
        [Fact]
        public async void Insert_InsertUnsuccessfull_ReturnsBadRequest()
        {
            // Arrange

            var userInsertRequest = new UserInsertDto();

            var userInsertResult = Result<UserDto>.Error("Failed to insert user!");

            _userService.Setup(_ => _.Insert(userInsertRequest)).ReturnsAsync(userInsertResult);

            // Act

            var endpointResult = await _usersController.Insert(userInsertRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<BadRequestObjectResult>(endpointResult);
        }

        #endregion Insert
    }
}
