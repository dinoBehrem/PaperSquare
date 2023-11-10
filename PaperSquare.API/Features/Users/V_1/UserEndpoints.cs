﻿namespace PaperSquare.API.Features.Users.V_1;

public static class UserEndpoints
{
    private const string user = "user";
    private const string user_path = $"api/{user}";
    private const string user_tag_name = "User endpoints";

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(user_path)
            .WithTags(user_tag_name)            
            .WithOpenApi();

        group.MapGetAllUsers();

        group.MapGetUserById();       
        
        group.MapCreateUser();

        group.MapUpdateUser();

        group.MapDeleteUser();

        return routes;
    }
}