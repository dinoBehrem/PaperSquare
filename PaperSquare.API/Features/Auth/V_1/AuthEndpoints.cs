namespace PaperSquare.API.Features.Auth.V_1;

public static class AuthEndpoints
{
    private const string auth = "auth";
    private const string auth_path = $"api/{auth}";
    private const string auth_tag_name = "Auth endpoints";

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(auth_path)
            .AllowAnonymous()
            .WithTags(auth_tag_name)
            .WithOpenApi();

        group.MapLogin();
        group.MapRefreshToken();
        group.MapVerifyAccount();

        return routes;
    }
}
