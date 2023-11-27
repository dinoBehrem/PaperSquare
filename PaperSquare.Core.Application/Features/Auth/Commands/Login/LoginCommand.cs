using Ardalis.Result;
using MediatR;
using PaperSquare.Core.Application.Shared.Dto;

namespace PaperSquare.Core.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string username, string password): IRequest<Result<AuthResponse>>;
