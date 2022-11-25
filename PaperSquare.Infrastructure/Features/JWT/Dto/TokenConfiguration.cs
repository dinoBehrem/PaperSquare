using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT.Dto
{
    public class TokenConfiguration
    {
        public TimeSpan TokenDuration { get; set; }
        public TimeSpan RefreshTokenDuration { get; set; }
        public SymmetricSecurityKey SecurityKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public string SecretKey = "KqcL7s998JrfFHRP1NaondfarnJMPO$&sdf!";
    }
}
