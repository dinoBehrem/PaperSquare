using Ardalis.Result;
using PaperSquare.API.Feature.Auth.Dto;
using PaperSquare.Infrastructure.Features.Auth.Dto;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> Login(LoginInsertRequest request);
        Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request);
    }
}
