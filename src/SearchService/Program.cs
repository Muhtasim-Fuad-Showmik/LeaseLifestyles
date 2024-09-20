using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

// Middleware for directing the HTTP request to the correct API endpoint
app.MapControllers();

await DB.InitAsync("SearchDb", MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDbConnection")));

await DB.Index<Item>()
    .Key(x => x.Address, KeyType.Text)
    .Key(x => x.FloorNumber, KeyType.Text)
    .Key(x => x.HouseSize, KeyType.Text)
    .CreateAsync();

app.Run();
