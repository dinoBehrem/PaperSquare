using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Exceptions
{
    public class UnatuhorizedAccessException : CustomException
    {
        public UnatuhorizedAccessException(string message): base(message, null, HttpStatusCode.Unauthorized)
        {
            
        }
    }
}
