using KnowledgeHub.Data.Entities.Auth;
using KnowledgeHub.Data.Extensions;
using KnowledgeHub.Repository.Extensions;
using KnowledgeHub.Services.Extensions;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true,
        reloadOnChange: true).AddEnvironmentVariables();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null) builder.Services.AddDbContextExtension(connectionString);
//add sheet data test
var hasher = new PasswordHasher<User>();

Console.WriteLine("admin: " + hasher.HashPassword(null!, "123456"));
Console.WriteLine("thuydv: " + hasher.HashPassword(null!, "123456"));
// DI theo layer
builder.Services.AddRepositories();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map OpenAPI JSON
    app.MapOpenApi();

    // 🔥 Scalar UI
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("KnowledgeHub API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();