using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using PaperSquare.Infrastructure.Features.JWT;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System.Security.Claims;

namespace PaperSquare.Infrastructure.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(
            SignInManager<User> signInManager,
            ITokenService tokenService,
            UserManager<User> userManager,
            IRefreshTokenService refreshTokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<AuthResponse>> Login(LoginInsertRequest request)
        {
            Guard.Against.Null(request, nameof(request));

            var user = await _userManager.FindByNameAsync(request.Username);

            if (!IsValidUser(user))
            {
                return Result<AuthResponse>.Error("It`s not a valid account!");
            }

            if (!await _signInManager.CanSignInAsync(user))
            {
                return Result<AuthResponse>.Error("You haven`t confirmed your account!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
            {
                return Result<AuthResponse>.Error("Incorrect username or password!!");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim("Role", role)));

            var accesToken = await _tokenService.BuildToken(claims);

            var refreshToken = await _tokenService.BuildRefreshToken(user);

            var authResponse = new AuthResponse()
            {
                AccessToken = accesToken,
                RefreshToken = refreshToken
            };

            return authResponse;
        }

        public async Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            Guard.Against.Null(request, nameof(request));

            var token = await _refreshTokenService.GetToken(request.Token);

            if (token is null)
            {
                return Result<AuthResponse>.Error("Refresh token doesn`t exist!");
            }

            var user = await _userManager.FindByNameAsync(token.UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim("Role", role)));

            var accesToken = await _tokenService.BuildToken(claims);

            var refreshToken = await _tokenService.BuildRefreshToken(user);

            var authResponse = new AuthResponse()
            {
                AccessToken = accesToken,
                RefreshToken = refreshToken
            };

            await _refreshTokenService.MarkAsInvalid(token);

            return authResponse;
        }

        private bool IsValidUser(User user)
        {
            return user != null && !user.IsDeleted;
        }
    }
}
