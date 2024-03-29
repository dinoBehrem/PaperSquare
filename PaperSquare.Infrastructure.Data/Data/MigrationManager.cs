﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaperSquare.Infrastructure.Data.Data;
using Serilog;

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
                        if (!app.Environment.IsEnvironment("Testing"))
                        {
                            dbContext.Database.Migrate();
                        }
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
