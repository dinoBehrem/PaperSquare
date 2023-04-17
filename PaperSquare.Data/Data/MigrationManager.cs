using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Data.Data
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                using(var dbContext = scope.ServiceProvider.GetRequiredService<PaperSquareDbContext>())
                {
                    try
                    {
                        dbContext.Database.Migrate();
                    }
                    catch (Exception exc)
                    {
                        Log.Warning("Failed to seed database");
                        Log.Error($"Message: {exc.Message} \n Inner message: {exc.InnerException?.Message}");
                        throw;
                    }
                }
            }

            return app;
        }
    }
}
