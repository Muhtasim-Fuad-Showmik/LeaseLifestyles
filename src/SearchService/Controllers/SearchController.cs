using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    /// <summary>
    /// This method searches all items in the Items collection based on a search term.
    /// </summary>
    /// <param name="searchTerm">The search term to search for.</param>
    /// <returns>A list of items that match the search term.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm)
    {
        // Search all items in the Items collection
        var query = DB.Find<Item>();

        // Sort the items by their addresses in ascending order
        query.Sort(x => x.Ascending(a => a.Address));

        // If a search term is provided, filter the result based on the search term
        if (!string.IsNullOrEmpty(searchTerm))
        {
            // Use the full text search to search for the search term
            // Sort the result by the text score in descending order
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }

        // Execute the query and store the result
        var result = await query.ExecuteAsync();

        // Return the result of the query
        return result;
    }
}

