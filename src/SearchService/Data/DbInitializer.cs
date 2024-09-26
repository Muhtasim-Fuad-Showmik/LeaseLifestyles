using System;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

/// <summary>
/// Class for initializing the database with data from the Rent service
/// </summary>
public class DbInitializer
{
    /// <summary>
    /// Initializes the database with data from the Rent service
    /// </summary>
    /// <param name="app">The WebApplication object</param>
    public static async Task InitDb(WebApplication app)
    {
        // Initialize database using the connection string
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        // Index database by address, floor number and house size
        await DB.Index<Item>()
            .Key(x => x.Address, KeyType.Text)
            .Key(x => x.FloorNumber, KeyType.Text)
            .Key(x => x.HouseSize, KeyType.Text)
            .CreateAsync();

        // Create a scope to get the services within the application
        using var scope = app.Services.CreateScope();

        // Get the RentServiceHttpClient service
        var httpClient = scope.ServiceProvider
            .GetRequiredService<RentServiceHttpClient>();

        // Get the items from the Rent service (only untracked updated ones)
        var items = await httpClient.GetItemsForSearchDb();

        // TODO: Print the number of items returned. Remove when no longer necessary
        Console.WriteLine(items.Count + " returned from the Rent Service");

        // Save the items to the database if there are any
        if (items.Count > 0) await DB.SaveAsync(items);
    }
}

