using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleApi.Authentication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information); // Filter for EF Core queries
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataContext"))
        .UseLoggerFactory(loggerFactory)
        .EnableSensitiveDataLogging());

// NB: Authentication
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapIdentityApi<IdentityUser>(); // NB: Authentication

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
