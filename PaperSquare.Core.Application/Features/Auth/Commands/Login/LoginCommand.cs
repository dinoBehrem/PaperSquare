using MediatR;
using PaperSquare.Core.Application.Features.Common;

namespace PaperSquare.Core.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string username, string password): IRequest<AuthResponse>;
