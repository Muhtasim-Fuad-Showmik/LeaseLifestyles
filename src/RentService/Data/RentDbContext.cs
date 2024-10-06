using System;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RentService.Entities;

namespace RentService.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Rent> Rents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add the MassTransit inbox state to the database
        modelBuilder.AddInboxStateEntity();

        // Add the MassTransit outbox message to the database
        modelBuilder.AddOutboxMessageEntity();

        // Add the MassTransit outbox state to the database
        modelBuilder.AddOutboxStateEntity();
    }
}
