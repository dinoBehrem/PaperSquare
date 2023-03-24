using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using PaperSquare.Infrastructure.Features.JWT;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.UnitTests.System.Infrastrucutre.Auth
{
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        private readonly Mock<SignInManager<User>> _signInManager;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<IRefreshTokenService> _refreshTokenService;

        public AuthServiceTest()
        {
            // Mocking dependencies
            _userManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _signInManager = new Mock<SignInManager<User>>(_userManager.Object,new Mock<IHttpContextAccessor>().Object, new Mock<IUserClaimsPrincipalFactory<User>>().Object, null, null, null, null);

            _tokenService = new Mock<ITokenService>();
            _refreshTokenService = new Mock<IRefreshTokenService>();

            // SUT
            _authService = new AuthService(_signInManager.Object, _tokenService.Object, _userManager.Object, _refreshTokenService.Object);
        }

        #region Login

        [Fact]
        public async void Login_SuccessfullLogin_ReturnsResultSuccess()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "JohnDoe",
                Password = "johnDoe!1"
            };

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = false
            };

            var claims = new List<Claim>()
            {
                new Claim(AppClaimTypes.Id, user.Id),
                new Claim(AppClaimTypes.UserName, user.UserName),
                new Claim(AppClaimTypes.Email, user.Email)
            };

            var tokenResource = new TokenResource()
            {
                Expiriation = DateTime.Now.AddMinutes(10),
                Token = Guid.NewGuid().ToString(),
            };

            var signInResult = SignInResult.Success;

            _userManager.Setup(_ => _.FindByNameAsync(loginInsertRequest.Username)).ReturnsAsync(user);

            _userManager.Setup(_ => _.GetRolesAsync(user)).ReturnsAsync(new List<string>() { Roles.RegisteredUser });

            _signInManager.Setup(_ => _.CanSignInAsync(user)).ReturnsAsync(true);

            _signInManager.Setup(_ => _.CheckPasswordSignInAsync(user, loginInsertRequest.Password, true)).ReturnsAsync(signInResult);

            _tokenService.Setup(_ => _.BuildToken(claims)).ReturnsAsync(tokenResource);
            
            _tokenService.Setup(_ => _.BuildRefreshToken(user)).ReturnsAsync(tokenResource);

            // Act

            var serviceResult = await _authService.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(serviceResult.IsSuccess);
        }

        [Fact]
        public async void Login_InvalidUser_ReturnsResultError()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "JohnDoe",
                Password = "johnDoe!1"
            };

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = true
            };

            _userManager.Setup(_ => _.FindByNameAsync(loginInsertRequest.Username)).ReturnsAsync(user);

            // Act

            var serviceResult = await _authService.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        [Fact]
        public async void Login_SignInFailed_ReturnsResultError()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "JohnDoe",
                Password = "johnDoe!1"
            };

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = false
            };

            _userManager.Setup(_ => _.FindByNameAsync(loginInsertRequest.Username)).ReturnsAsync(user);

            _signInManager.Setup(_ => _.CanSignInAsync(user)).ReturnsAsync(false);

            // Act

            var serviceResult = await _authService.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        [Fact]
        public async void Login_PasswordDoesntMatch_ReturnsResultError()
        {
            // Arrange

            var loginInsertRequest = new LoginInsertRequest()
            {
                Username = "JohnDoe",
                Password = "johnDoe!1"
            };

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = false
            };

            var signInResult = SignInResult.Failed;

            _userManager.Setup(_ => _.FindByNameAsync(loginInsertRequest.Username)).ReturnsAsync(user);

            _signInManager.Setup(_ => _.CanSignInAsync(user)).ReturnsAsync(true);

            _signInManager.Setup(_ => _.CheckPasswordSignInAsync(user, loginInsertRequest.Password, true)).ReturnsAsync(signInResult);

            // Act

            var serviceResult = await _authService.Login(loginInsertRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        #endregion Login

        #region RefreshToken

        [Fact]
        public async void RefreshToken_ValidRefreshToken_ReturnsResultSuccess()
        {
            // Arrange 

            var refreshTokenRequest = new RefreshTokenRequest()
            {
                Token = Guid.NewGuid().ToString()
            };

            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(10),
                IsValid = true,
                UserId = Guid.NewGuid().ToString()
            };

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = false
            };

            var claims = new List<Claim>()
            {
                new Claim(AppClaimTypes.Id, user.Id),
                new Claim(AppClaimTypes.UserName, user.UserName),
                new Claim(AppClaimTypes.Email, user.Email)
            };

            var tokenResource = new TokenResource()
            {
                Expiriation = DateTime.Now.AddMinutes(10),
                Token = Guid.NewGuid().ToString(),
            };

            _refreshTokenService.Setup(_ => _.GetToken(refreshTokenRequest.Token)).ReturnsAsync(refreshToken);

            _userManager.Setup(_ => _.FindByIdAsync(refreshToken.UserId)).ReturnsAsync(user);

            _userManager.Setup(_ => _.GetRolesAsync(user)).ReturnsAsync(new List<string>() { Roles.RegisteredUser });

            _tokenService.Setup(_ => _.BuildToken(claims)).ReturnsAsync(tokenResource);

            _tokenService.Setup(_ => _.BuildRefreshToken(user)).ReturnsAsync(tokenResource);

            _refreshTokenService.Setup(_ => _.MarkAsInvalid(refreshToken)).Returns(Task.CompletedTask);

            // Act 

            var serviceResult = await _authService.RefreshToken(refreshTokenRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Ok);
        }
        
        [Fact]
        public async void RefreshToken_InvalidRefreshToken_ReturnsResultError()
        {
            // Arrange 

            var refreshTokenRequest = new RefreshTokenRequest()
            {
                Token = Guid.NewGuid().ToString()
            };

            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(10),
                IsValid = false,
                UserId = Guid.NewGuid().ToString()
            };

            _refreshTokenService.Setup(_ => _.GetToken(refreshTokenRequest.Token)).ReturnsAsync(refreshToken);                 
            // Act 

            var serviceResult = await _authService.RefreshToken(refreshTokenRequest);

            // Assert

            Assert.NotNull(serviceResult);
            Assert.IsType<Result<AuthResponse>>(serviceResult);
            Assert.True(!serviceResult.IsSuccess);
            Assert.True(serviceResult.Status == ResultStatus.Error);
        }

        #endregion RefreshToken
    }
}
