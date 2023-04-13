using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Exceptions
{
    public class NotFoundEntityException: CustomException
    {
        public Type Type { get;}
        public NotFoundEntityException(string message, Type type): base(message, null, HttpStatusCode.NotFound)
        {           
            Type = type;
        }
    }
}
