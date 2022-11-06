using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Identity
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsValid { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
