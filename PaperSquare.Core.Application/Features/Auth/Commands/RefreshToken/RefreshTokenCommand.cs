using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Shared.Dto;

namespace PaperSquare.Core.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string token): IRequest<Result<AuthResponse>>{}
