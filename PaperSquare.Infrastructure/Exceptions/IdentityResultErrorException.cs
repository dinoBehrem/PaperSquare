using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Exceptions
{
    public class IdentityResultErrorException: CustomException
    {
        public List<string> Messages { get; set; }
        public IdentityResultErrorException(params string[] messages): base(messages.First())
        {
            Messages = messages.ToList();
        }
    }
}
