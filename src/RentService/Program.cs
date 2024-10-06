using Microsoft.EntityFrameworkCore;
using RentService.Data;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<RentDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x =>
{
    // Set up the Entity Framework Outbox which is used to store messages in the database
    // that are yet to be sent. This is useful if the application is shut down before all
    // messages can be sent.
    x.AddEntityFrameworkOutbox<RentDbContext>(o =>
    {
        // Delay each message in the outbox by 10 seconds before sending it
        o.QueryDelay = TimeSpan.FromSeconds(10);

        // Use Postgres as the database provider
        o.UsePostgres();

        // Use the outbox as the bus
        o.UseBusOutbox();
    });

    // Set up the RabbitMQ message bus
    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure the endpoints for the RabbitMQ message bus
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

// Middleware for directing the HTTP request to the correct API endpoint
app.MapControllers();

// Seed dummy data into the application
try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();