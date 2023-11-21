using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PaperSquare.API.Infrastructure.AppServices;
using PaperSquare.API.Infrastructure.Auth;
using PaperSquare.API.Infrastructure.Endpoints;
using PaperSquare.API.Infrastructure.HttpContext;
using PaperSquare.API.Infrastructure.Middlewares;
using PaperSquare.API.Infrastructure.SwaggerGen;
using PaperSquare.API.Middlewares.RateLimiting;
using PaperSquare.Core.Application;
using PaperSquare.Core.Application.Profiles;
using PaperSquare.Data.Data;
using PaperSquare.Data.Interceprots;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", false)
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Elasticsearch("http://localhost:9200", "papersquare-api", autoRegisterTemplate:true, numberOfReplicas:2, numberOfShards:2)
    .CreateLogger();

builder.Host.UseSerilog(logger);

// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.ApiVersioningConfiguration();

builder.Services.AddScoped<PaperSquareSaveChangesInterceptor>();
builder.Services.AddDbContext<PaperSquareDbContext>((serviceProvider, options) => 
{
    var saveChangesInterceptor = serviceProvider.GetService<PaperSquareSaveChangesInterceptor>()!;

    options.UseNpgsql(builder.Configuration.GetConnectionString("DevBase"));
    options.AddInterceptors(saveChangesInterceptor);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGenConfig();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AppServices();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddAuthorizationConfig();
builder.Services.CurrentPrincipalAccessorConfig();
builder.Services.AddCors();
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddExceptionConfig();
builder.Services.AddApplicationDependencies();

var app = builder.Build();

app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors(options => options
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.UseMiddlewareHandlers();

//app.MigrateDatabase();

app.UseAppEndpoints();

app.Run();

Log.CloseAndFlush();

public partial class Program { }