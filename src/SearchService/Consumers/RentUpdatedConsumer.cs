using System;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// This consumer listens for RentUpdated messages and updates the corresponding item in the MongoDB database.
/// </summary>
public class RentUpdatedConsumer : IConsumer<RentUpdated>
{
    private readonly IMapper _mapper;

    public RentUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// This method is called when a RentUpdated message is received.
    /// </summary>
    /// <param name="context">The message context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<RentUpdated> context)
    {
        Console.WriteLine("--> Consuming rent updated: " + context.Message);

        // Map the RentUpdated message to an Item entity
        var item = _mapper.Map<Item>(context.Message);

        // Update the item in the MongoDB database
        var result = await DB.Update<Item>()
            .Match(a => a.ID == context.Message.Id)
            .ModifyOnly(x => new
            {
                x.Address,
                x.FloorNumber,
                x.Beds,
                x.Baths,
                x.Balconies,
                x.HouseSize,
                x.HouseSizeUnit,
                x.LandSize,
                x.LandSizeUnit,
                x.Description,
                x.AvailableFrom
            }, item)
            .ExecuteAsync();

        // Check if the update was successful
        if (!result.IsAcknowledged)
            throw new MessageException(typeof(RentUpdated), "Problem updating mongodb");
    }
}

