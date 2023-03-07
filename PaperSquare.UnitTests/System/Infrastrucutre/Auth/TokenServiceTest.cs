using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Moq;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
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
    public class TokenServiceTest
    {
        private readonly ITokenService _tokenService;
        private readonly Mock<IRefreshTokenService> _refreshTokenService;
        private readonly IOptions<TokenConfiguration> _tokenConfiguration;

        public TokenServiceTest()
        {
            // Mocking dependenices

            _refreshTokenService = new Mock<IRefreshTokenService>();
            _tokenConfiguration = Options.Create(new TokenConfiguration());

            // SUT

            _tokenService = new TokenService(_tokenConfiguration, _refreshTokenService.Object);
        }

        #region BuildToken

        [Fact]
        public async void BuildToken_GenerateToken_ReturnsTokenResource()
        {
            // Arrange

            var claims = new List<Claim>()
            {
                new Claim(AppClaimTypes.Id, Guid.NewGuid().ToString()),
                new Claim(AppClaimTypes.UserName, "johnDoe"),
                new Claim(AppClaimTypes.Email, "john.doe@mail.com")
            };

            // Act 

            var serviceResult = await _tokenService.BuildToken(claims);

            // Assert

            Assert.NotNull(serviceResult);
        }

        #endregion BuildToken

        #region BuildRefreshToken

        [Fact]
        public async void BuildRefreshToken_GenerateRefreshToken_ReturnsTokenResource()
        {
            // Arrange 

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "johnDoe",
                IsDeleted = false
            };

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(10),
            };

            _refreshTokenService.Setup(_ => _.AddRefreshToken(refreshToken)).Returns(Task.CompletedTask);

            // Act

            var serviceResult = await _tokenService.BuildRefreshToken(user);

            // Assert

            Assert.NotNull(serviceResult);
        }

        #endregion BuildRefreshToken
    }
}
