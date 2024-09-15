using Microsoft.EntityFrameworkCore;
using RentService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<RentDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

// Middleware for directing the HTTP request to the correct API endpoint
app.MapControllers();

app.Run();