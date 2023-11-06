using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.API.Features.Auth.V_1;
using PaperSquare.Core.Application.Features.Common;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.Auth.Dto;

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

        #region Login

        //[Fact]
        //public async void Login_WithValidModel_ReturnsOk()
        //{
        //    // Arrange

        //    var loginInsertRequest = new LoginInsertRequest()
        //    {
        //        Username = "admin",
        //        Password = "administrator"
        //    };

        //    var authresponse = AuthResponse.Create()
        //    {
        //        AccessToken = new TokenResource
        //        {
        //            Expiriation = DateTime.Now.AddMinutes(5),
        //            Token = "superStrongJwtToken"
        //        },
        //        RefreshToken = new TokenResource
        //        {
        //            Expiriation = DateTime.Now.AddMinutes(5),
        //            Token = "tillTheEndOfTimeRefreshToken"
        //        }
        //    };

        //    var authResult = Result<AuthResponse>.Success(authresponse);

        //    _authService.Setup(_ => _.Login(loginInsertRequest)).ReturnsAsync(authResult);

        //    // Act

        //    var endpointResult = await _authController.Login(loginInsertRequest);

        //    var result = endpointResult as OkObjectResult;

        //    // Assert

        //    Assert.NotNull(endpointResult);
        //    Assert.IsType<OkObjectResult>(endpointResult);
        //    Assert.NotNull(result);
        //    Assert.IsType<AuthResponse>(result.Value);
        //}

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

        #endregion Login

        #region RefreshToken

        //[Fact]
        //public async void RefreshToken_ValidRefreshToken_ReturnOk()
        //{
        //    // Arrange

        //    var refreshTokenRequest = new RefreshTokenRequest()
        //    {
        //        Token = "someValidToken.ForTestingPurposes"
        //    };

        //    var authResponse = new AuthResponse()
        //    {
        //        AccessToken = new TokenResource
        //        {
        //            Expiriation = DateTime.Now.AddMinutes(5),
        //            Token = "superStrongJwtToken"
        //        },
        //        RefreshToken = new TokenResource
        //        {
        //            Expiriation = DateTime.Now.AddMinutes(5),
        //            Token = "tillTheEndOfTimeRefreshToken"
        //        }
        //    };

        //    var refreshTokenResult = Result<AuthResponse>.Success(authResponse);

        //    _authService.Setup(_ => _.RefreshToken(refreshTokenRequest)).ReturnsAsync(refreshTokenResult);

        //    // Act

        //    var endpointResult = await _authController.RefreshToken(refreshTokenRequest);

        //    // Assert

        //    Assert.NotNull(endpointResult); 
        //    Assert.IsType<OkObjectResult>(endpointResult);
        //}

        [Fact]
        public async void RefreshToken_InvalidToken_ReturnsBadRequest()
        {
            // Arrange 

            var refreshTokenRequest = new RefreshTokenRequest()
            {
                Token = ""
            };

            // Act 

            var endpointresult = await _authController.RefreshToken(refreshTokenRequest);

            // Assert

            Assert.NotNull(endpointresult); 
            Assert.IsType<BadRequestObjectResult>(endpointresult);
        }

        [Fact]
        public async void RefreshToken_ExpiredToken_ReturnBadRequest()
        {
            // Arrange

            var refreshTokenRequest = new RefreshTokenRequest()
            {
                Token = "expiredRefershToken"
            };

            var authresponse = Result<AuthResponse>.Error("Token has expired!");

            _authService.Setup(_ => _.RefreshToken(refreshTokenRequest)).ReturnsAsync(authresponse);

            // Act

            var endpointResult = await _authController.RefreshToken(refreshTokenRequest);

            //Assert

            Assert.NotNull(endpointResult);
            Assert.IsType<BadRequestObjectResult>(endpointResult);
        }

        #endregion RefreshToken
    }
}
