using Microsoft.EntityFrameworkCore;
using PaperSquare.API.Infrastructure.AppServices;
using PaperSquare.API.Infrastructure.Auth;
using PaperSquare.API.Infrastructure.SwaggerGen;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Profiles;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ApiVersioningConfiguration();
builder.Services.AddDbContext<PaperSquareDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevBase")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGenConfig();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AppServices();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddAuthorizationConfig(builder.Configuration);
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();
