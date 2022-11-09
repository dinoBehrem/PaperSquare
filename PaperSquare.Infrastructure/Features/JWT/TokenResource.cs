using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.JWT
{
    public class TokenResource
    {
        public string Token { get; set; }
        public DateTime Expiriation { get; set; }
    }
}
