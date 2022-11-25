using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using System.Security.Claims;

namespace PaperSquare.API.Infrastructure.HttpContext
{
    public class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal Principal => _contextAccessor.HttpContext?.User;
    }
}
