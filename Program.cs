using RareAPI.Endpoints;
using RareAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<PostService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbService = scope.ServiceProvider.GetRequiredService<DatabaseService>();
    await dbService.InitializeDatabaseAsync();
}

app.MapGet("/", () => "Welcome to Rare Publishing Platform API!");

app.MapAuthEndpoints();
app.MapPostEndpoints();

app.Run();
