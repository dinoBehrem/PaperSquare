﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Exceptions;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using System.Security.Claims;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Application.Features.JWT;
using PaperSquare.Core.Application.Features.Common;
using PaperSquare.Core.Application.Features.JWT.Dto;
using Microsoft.Extensions.Options;

namespace PaperSquare.Infrastructure.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TokenConfiguration _tokenConfiguration;

        public AuthService(
            SignInManager<User> signInManager,
            ITokenService tokenService,
            UserManager<User> userManager,
            IRefreshTokenService refreshTokenService,
            IOptions<TokenConfiguration> tokenConfiguratiuon)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
            _tokenConfiguration = tokenConfiguratiuon.Value;
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
                throw new BadRequestException("You haven`t confirmed your account!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Incorrect username or password!");
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

            var authResponse = AuthResponse.Create(claims, user, _tokenConfiguration);

            return Result<AuthResponse>.Success(authResponse);
        }

        public async Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            Guard.Against.Null(request, nameof(request));

            var token = await _refreshTokenService.GetToken(request.Token);

            if (token is null || !token.IsValid)
            {
                throw new BadRequestException("You haven`t confirmed your account!");
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

            var authResponse = AuthResponse.Create(claims, user, _tokenConfiguration);

            await _refreshTokenService.MarkAsInvalid(token);

            return Result<AuthResponse>.Success(authResponse);
        }

        private bool IsValidUser(User user)
        {
            return user != null && !user.IsDeleted;
        }
    }
}
