using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

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
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        // Search all items in the Items collection with pagination
        var query = DB.PagedSearch<Item, Item>();

        // Sort the items by their addresses in ascending order
        query.Sort(x => x.Ascending(a => a.Address));

        // If a search term is provided, filter the result based on the search term
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            // Use the full text search to search for the search term
            // Sort the result by the text score in descending order
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        // Apply sorting based on the order parameter
        query = searchParams.OrderBy switch
        {
            "address" => query.Sort(x => x.Ascending(a => a.Address)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            "emergency" => query.Sort(x => x.Descending(a => a.AvailableFrom)),
            _ => query.Sort(x => x.Ascending(a => a.ConfirmBy)),
        };

        // Apply filtering based on the filterBy parameter
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.ConfirmBy < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.ConfirmBy < DateTime.UtcNow.AddHours(6)
                && x.ConfirmBy > DateTime.UtcNow),
            _ => query.Match(x => x.ConfirmBy > DateTime.UtcNow),
        };

        // Apply search parameters for any specified landlord
        if (!string.IsNullOrEmpty(searchParams.Landlord))
        {
            query.Match(x => x.Landlord == searchParams.Landlord);
        }

        // Apply search parameters for any specified tennant
        if (!string.IsNullOrEmpty(searchParams.Tennant))
        {
            query.Match(x => x.Tennant == searchParams.Tennant);
        }

        // Apply pagination configurations
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        // Execute the query and store the result
        var result = await query.ExecuteAsync();

        // Return the result of the query with pagination data
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}

