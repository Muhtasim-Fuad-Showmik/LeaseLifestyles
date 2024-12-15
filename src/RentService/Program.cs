using Microsoft.EntityFrameworkCore;
using RentService.Data;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RentService.Consumers;

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

    // Add all consumers from the same namespace as the RentCreatedFaultConsumer
    x.AddConsumersFromNamespaceContaining<RentCreatedFaultConsumer>();

    // Set the endpoint name formatter to use a kebab case (lowercase and hyphenated)
    // format for the endpoint names. The first parameter is the prefix to use for
    // the endpoint names, and the second parameter is a boolean that determines
    // whether to append the endpoint name with the namespace of the consumer.
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("rent", false));

    // Set up the RabbitMQ message bus
    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure the endpoints for the RabbitMQ message bus
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        
        // As our service is running on HTTP not HTTPS
        options.RequireHttpsMetadata = false;
        
        options.TokenValidationParameters.ValidateAudience = false;
        
        // Exactly as specified in our Identity Service custom profile
        options.TokenValidationParameters.NameClaimType = "username";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

// Authenticate the user and then authorize for granting access
app.UseAuthentication();
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