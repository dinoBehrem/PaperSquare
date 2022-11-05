using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(
            SignInManager<User> signInManager, 
            ITokenService tokenService,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
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

            var token = await _tokenService.BuildToken(claims);

            return token;
        }

        private bool IsValidUser(User user)
        {
            return user != null && !user.IsDeleted;
        }
    }
}
