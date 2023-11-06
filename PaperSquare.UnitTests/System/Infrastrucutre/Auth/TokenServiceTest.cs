using Microsoft.Extensions.Options;
using Moq;
using PaperSquare.Core.Application.Features.JWT;
using PaperSquare.Core.Permissions;
using PaperSquare.Core.Domain.Entities.Identity;
using System.Security.Claims;
using PaperSquare.Core.Application.Features.JWT.Dto;

namespace PaperSquare.UnitTests.System.Infrastrucutre.Auth;

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

        var user = new User("John", "Doe", "johnDoe", "john.doe@mail.com");

        var refreshToken = new RefreshToken(id: Guid.NewGuid().ToString(), userId: Guid.NewGuid().ToString(), DateTime.UtcNow.AddMinutes(10));

        _refreshTokenService.Setup(_ => _.AddRefreshToken(refreshToken)).Returns(Task.CompletedTask);

        // Act

        var serviceResult = await _tokenService.BuildRefreshToken(user);

        // Assert

        Assert.NotNull(serviceResult);
    }

    #endregion BuildRefreshToken
}
