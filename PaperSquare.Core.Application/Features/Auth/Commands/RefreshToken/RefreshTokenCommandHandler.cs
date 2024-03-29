﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaperSquare.Core.Application.Exceptions;
using PaperSquare.Core.Application.Features.JWT.Dto;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Application.Shared.Dto;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using System.Security.Claims;

namespace PaperSquare.Core.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    private readonly IPaperSquareDbContext _context;
    private readonly TokenConfiguration _tokenConfiguration;
    private readonly ICurrentUser _currentUser;

    public RefreshTokenCommandHandler(IPaperSquareDbContext context, IOptions<TokenConfiguration> tokenConfiguration, ICurrentUser currentUser)
    {
        _tokenConfiguration = tokenConfiguration.Value;
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var user = await _context.Users.Include(u => u.RefreshTokens)
                                       .Include(u => u.Roles)
                                       .FirstOrDefaultAsync(u => u.Equals(_currentUser.Id), cancellationToken);

        if (user is null)
        {
            throw new InternalServerException("Unknown error occured!");
        }

        var token = user.RefreshTokens.FirstOrDefault(rt => rt.Id == request.token);

        if (token is null || !token.IsValid)
        {
            throw new BadRequestException("You haven`t confirmed your account!");
        }

        var roles = user.Roles.Select(u => u.Role.Name).ToList();

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id)
        };

        claims.AddRange(roles.Select(role => new Claim("Role", role)));

        var authResponse = AuthResponse.Create(claims, user, _tokenConfiguration);

        token.MarkAsInvalid();

        return Result<AuthResponse>.Success(authResponse);
    }
}
