using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services
{
    /// <summary>
    /// A service for interacting with the Rent service API to get a list of items
    /// that are updated at a date later than the last updated date of the last item
    /// in the Search database.
    /// </summary>
    public class RentServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        /// <summary>
        /// Creates a new instance of the RentServiceHttpClient class.
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for making HTTP requests.</param>
        /// <param name="config">The IConfiguration object to get the Rent service URL from.</param>
        public RentServiceHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        /// <summary>
        /// Gets the list of items from the Rent service that are updated at a date later than
        /// the last updated date of the last item in the Search database.
        /// </summary>
        /// <returns>A list of Item objects representing the rents that match the criteria.</returns>
        public async Task<List<Item>> GetItemsForSearchDb()
        {
            // Get the date of the last updated Rent item from the database
            var lastUpdated = await DB.Find<Item, string>()
                .Sort(x => x.Descending(x => x.UpdatedAt))
                .Project(x => x.UpdatedAt.ToString())
                .ExecuteFirstAsync();

            // Get the list of items from the Rent service that are 
            // updated at a date later than the last updated date
            return await _httpClient.GetFromJsonAsync<List<Item>>(
                _config["RentServiceUrl"] + "/api/rents?date=" + lastUpdated);
        }
    }
}

