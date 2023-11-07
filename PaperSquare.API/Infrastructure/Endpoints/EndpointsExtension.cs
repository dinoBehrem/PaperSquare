using PaperSquare.API.Features.Auth.V_1;
using PaperSquare.API.Features.Users.V_1;
using PaperSquare.API.Infrastructure.Versioning;

namespace PaperSquare.API.Infrastructure.Endpoints;

public static class EndpointsExtension
{
    public static IEndpointRouteBuilder UseAppEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGroup(ApiVersions.V_1)
            .MapUserEndpoints()
            .MapAuthEndpoints();

        return routes;
    }
}
