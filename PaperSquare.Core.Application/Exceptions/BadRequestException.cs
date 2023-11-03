using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message, statusCode: HttpStatusCode.BadRequest)
        {
        }
    }
}
