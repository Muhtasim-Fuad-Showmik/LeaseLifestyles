using System;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// A consumer for the <see cref="RentDeleted"/> message.
/// </summary>
public class RentDeletedConsumer : IConsumer<RentDeleted>
{
    /// <summary>
    /// Consumes the specified message and deletes the corresponding item in the database.
    /// </summary>
    /// <param name="context">The message context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<RentDeleted> context)
    {
        Console.WriteLine("--> Consuming rent deleted: " + context.Message.Id);

        // Delete the item from the database
        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        // If the deletion was not successful, throw an exception
        if (!result.IsAcknowledged)
            throw new MessageException(typeof(RentDeleted), "Problem deleting rent");
    }
}

