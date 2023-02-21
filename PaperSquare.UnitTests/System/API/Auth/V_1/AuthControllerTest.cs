using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.API.Features.Auth.V_1;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.UnitTests.System.API.Auth.V_1
{
    public class AuthControllerTest
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authService;

        public AuthControllerTest()
        {
            // Mocking dependencies
            _authService = new Mock<IAuthService>();

            // SUT
            _authController = new AuthController(_authService.Object);
        }

        [Fact]
        public async void Login_WithValidModel_ReturnsOK()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "admin",
                Password = "administrator"
            };

            var authresponse = new AuthResponse()
            {
                AccessToken = new TokenResource
                {
                    Expiriation = DateTime.Now.AddMinutes(5),
                    Token = "superStrongJwtToken"
                },
                RefreshToken = new TokenResource
                {
                    Expiriation = DateTime.Now.AddMinutes(5),
                    Token = "tillTheEndOfTimeRefreshToken"
                }
            };

            var authResult = Result<AuthResponse>.Success(authresponse);

            _authService.Setup(_ => _.Login(loginInsertRequest)).ReturnsAsync(authResult);

            // Act

            var endpointResult = await _authController.Login(loginInsertRequest);

            var result = endpointResult as OkObjectResult;

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<OkObjectResult>(endpointResult);
            Assert.NotNull(result);
            Assert.IsType<AuthResponse>(result.Value);
        }

        [Fact]
        public async void Login_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = null,
                Password = ""
            };

            // Act

            var endpointResult = await _authController.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<BadRequestObjectResult>(endpointResult);
        }
        
        [Fact]
        public async void Login_WithValidModel_ReturnsNotFound()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "admin",
                Password = "administrator"
            };

            var authResult = Result<AuthResponse>.Error("It`s not a valid account!");

            _authService.Setup(_ => _.Login(loginInsertRequest)).ReturnsAsync(authResult);

            // Act

            var endpointResult = await _authController.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<NotFoundObjectResult>(endpointResult);
        }                
    }
}
