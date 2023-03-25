using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PaperSquare.Core.Models.Identity;
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

                using(var scope  = sp.CreateScope())
                
                using(var appContext =  scope.ServiceProvider.GetRequiredService<PaperSquareDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();

                        if (appContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                        {
                            appContext.Database.Migrate();
                        }

                        UsersSeedData(appContext);
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
                        Firstname = $"First name -- {i}",
                        Lastname = $"Last name -- {i}",
                        Email = $"testuser{i}@example.com"
                    });

                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
