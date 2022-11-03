using PaperSquare.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public class TokenService : ITokenService
    {
        public async Task<AuthResponse> BuildToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
