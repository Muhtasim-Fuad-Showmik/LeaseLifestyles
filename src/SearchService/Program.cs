using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<RentServiceHttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

// Middleware for directing the HTTP request to the correct API endpoint
app.MapControllers();

try
{
    // Start up the database and seed data into the database
    await DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
