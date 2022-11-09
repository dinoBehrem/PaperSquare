using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Permissions
{
    public static class Roles
    {
        public const string Admin = nameof(Admin);
        public const string Editor = nameof(Editor);
        public const string User = nameof(User);
        public const string Guest = nameof(Guest);
    }
}
