using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Exceptions;
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
                throw new NotFoundEntityException($"User with username: '{request.Username}' not found!", typeof(User));
            }

            if (!await _signInManager.CanSignInAsync(user))
            {
                throw new ErrorException("You haven`t confirmed your account!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
            {
                throw new ErrorException("Incorrect username or password!");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(AppClaimTypes.Id, user.Id),
                new Claim(AppClaimTypes.UserName, user.UserName),
                new Claim(AppClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(AppClaimTypes.Role, role)));

            var accesToken = await _tokenService.BuildToken(claims);

            var refreshToken = await _tokenService.BuildRefreshToken(user);

            var authResponse = new AuthResponse()
            {
                AccessToken = accesToken,
                RefreshToken = refreshToken
            };

            return Result<AuthResponse>.Success(authResponse);
        }

        public async Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            Guard.Against.Null(request, nameof(request));

            var token = await _refreshTokenService.GetToken(request.Token);

            if (token is null || !token.IsValid)
            {
                throw new ErrorException("You haven`t confirmed your account!");
            }

            var user = await _userManager.FindByIdAsync(token.UserId);
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

            return Result<AuthResponse>.Success(authResponse);
        }

        private bool IsValidUser(User user)
        {
            return user != null && !user.IsDeleted;
        }
    }
}
