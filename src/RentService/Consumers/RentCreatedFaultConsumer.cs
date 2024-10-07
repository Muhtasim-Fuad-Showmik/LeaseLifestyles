using System;
using Contracts;
using MassTransit;

namespace RentService.Consumers;

public class RentCreatedFaultConsumer : IConsumer<Fault<RentCreated>>
{
    public async Task Consume(ConsumeContext<Fault<RentCreated>> context)
    {
        Console.WriteLine("--> Consuming faulty creation");

        // Retrieve the first exception
        var exception = context.Message.Exceptions.First();

        // If it's an argument exception, we can try to correct it
        if (exception.ExceptionType == "System.ArgumentException")
        {
            // Correct the house size
            context.Message.Message.HouseSize = 500;

            // Send the corrected message back to the queue
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine("Not an argument exception - update error dashboard somewhere");
        }
    }
}

