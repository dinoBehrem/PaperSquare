using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.JWT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public interface ITokenService
    {
        Task<TokenResource> BuildToken(IEnumerable<Claim> claims);
        Task<TokenResource> BuildRefreshToken(User user);
    }   
}
