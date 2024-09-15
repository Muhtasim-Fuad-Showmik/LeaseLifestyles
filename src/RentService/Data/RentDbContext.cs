using System;
using Microsoft.EntityFrameworkCore;
using RentService.Entities;

namespace RentService.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Rent> Rents { get; set; }
}
