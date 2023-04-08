using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Data.Data;

namespace PaperSquare.IntegrationTest.WebApplicationBuilder
{
    public class PaperSquareAppFactory<TEntryPoiint> : WebApplicationFactory<Program> where TEntryPoiint : Program
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
                    dbContext.Users.Add(new User()
                    {
                        Id = $"user-{i}-id",
                        UserName = $"userName-{i}",
                        Firstname = $"First name -- {i}",
                        Lastname = $"Last name -- {i}",
                        Email = $"testuser{i}@example.com",
                        PasswordHash = "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==",
                        SecurityStamp = "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN",
                        ConcurrencyStamp = "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce",
                        EmailConfirmed = true
                    });

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
                        Id = $"role-{Roles.Admin}",
                        Name = Roles.Admin,
                        NormalizedName = Roles.Admin.ToUpper()
                    },
                    new Role()
                    {
                        Id = $"role-{Roles.RegisteredUser}",
                        Name = Roles.RegisteredUser,
                        NormalizedName = Roles.RegisteredUser.ToUpper()
                    },
                    new Role()
                    {
                        Id = $"role-{Roles.Guest}",
                        Name = Roles.Guest,
                        NormalizedName = Roles.Guest.ToUpper()
                    },
                    new Role()
                    {
                        Id = $"role-{Roles.Editor}",
                        Name = Roles.Editor,
                        NormalizedName = Roles.Editor.ToUpper()
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
}
