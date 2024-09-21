using System;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        // Initialize database using the connection string
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        // Index database by address, flood number and house size
        await DB.Index<Item>()
            .Key(x => x.Address, KeyType.Text)
            .Key(x => x.FloorNumber, KeyType.Text)
            .Key(x => x.HouseSize, KeyType.Text)
            .CreateAsync();

        // Check if data exists within the table
        var count = await DB.CountAsync<Item>();

        // For an empty table
        if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            //  Read JSON data from the stored JSON file
            var itemData = await File.ReadAllTextAsync("Data/rents.json");

            // State options for deserializing to be case insensitive
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Convert JSON data to a list collection of items
            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

            // Save items data into the database
            await DB.SaveAsync(items);
        }
    }
}
