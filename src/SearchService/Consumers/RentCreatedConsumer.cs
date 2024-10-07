using System;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// A consumer for the <see cref="RentCreated"/> message.
/// </summary>
public class RentCreatedConsumer : IConsumer<RentCreated>
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="RentCreatedConsumer"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance to use for mapping between objects.</param>
    public RentCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Consumes the specified message and saves it to the database
    /// </summary>
    /// <param name="context">The message context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<RentCreated> context)
    {
        Console.WriteLine("--> Consuming rent created: " + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);

        if (item.HouseSize < 500 && item.HouseSizeUnit == "Sqft") throw new ArgumentException("Cannot sell houses smaller than 500 square feet");

        await item.SaveAsync();
    }
}

