using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Core.Domain;

namespace PaperSquare.IntegrationTest.WebApplicationBuilder;

public class PaperSquareAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PaperSquareDbContext>));


            if (descriptor != null)
            {
                services.Remove(descriptor);
            }


            services.AddDbContext<PaperSquareDbContext>(options => options.UseInMemoryDatabase(nameof(PaperSquareDbContext)));

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())

            using (var appContext = scope.ServiceProvider.GetRequiredService<PaperSquareDbContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();

                    if (appContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        appContext.Database.Migrate();
                    }

                    UsersSeedData(appContext);
                    RolesSeedData(appContext);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        });

        base.ConfigureWebHost(builder);
    }

    private async void UsersSeedData(PaperSquareDbContext dbContext)
    {
        if (await dbContext.Users.CountAsync() <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                // TO DO: 
                dbContext.Users.Add(new User(PersonalInfo.Create($"First name -- {i}", $"Last name -- {i}", new DateTime(1,1, 2000 - i)), $"userName -- {i}", $"testuser{i}@example.com"));
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async void RolesSeedData(PaperSquareDbContext dbContext)
    {
        if (await dbContext.Roles.CountAsync() <= 0)
        {
            dbContext.Roles.AddRange(
                new Role()
                {
                    Id = $"role-{AppRoles.ADMIN}",
                    Name = AppRoles.ADMIN,
                    NormalizedName = AppRoles.ADMIN.ToUpper()
                },
                new Role()
                {
                    Id = $"role-{AppRoles.REGISTERED_USER}",
                    Name = AppRoles.REGISTERED_USER,
                    NormalizedName = AppRoles.REGISTERED_USER.ToUpper()
                },
                new Role()
                {
                    Id = $"role-{AppRoles.GUEST}",
                    Name = AppRoles.GUEST,
                    NormalizedName = AppRoles.GUEST.ToUpper()
                },
                new Role()
                {
                    Id = $"role-{AppRoles.EDITOR}",
                    Name = AppRoles.EDITOR,
                    NormalizedName = AppRoles.EDITOR.ToUpper()
                });
        }

        await dbContext.SaveChangesAsync();
    }

    private async void UserRolesSeedData(PaperSquareDbContext dbContext)
    {
        if(await dbContext.UserRoles.CountAsync() <= 0)
        {
            dbContext.UserRoles.AddRange(
                new UserRole()
            {
                RoleId = "role-Admin",
                UserId = "user-1-id"
            }, 
                new UserRole()
            {
                RoleId = "role-RegisteredUser",
                UserId = "user-1-id"
            });
        }

        await dbContext.SaveChangesAsync();
    }
}
