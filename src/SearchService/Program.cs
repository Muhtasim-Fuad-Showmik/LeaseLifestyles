using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Services;

// Create a new WebApplicationBuilder with the specified command-line arguments
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddControllers() adds the MVC services and configures the app to use controllers
builder.Services.AddControllers();

// Add AutoMapper to the service container and scan for assemblies
// AutoMapper is an Object-to-Object mapper which can be used to map
// between objects of different types.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add a new HttpClient to the service container
// AddPolicyHandler() adds a Polly policy to handle HTTP errors
builder.Services.AddHttpClient<RentServiceHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    // Add the RentCreatedConsumer namespace to locate consumers
    x.AddConsumersFromNamespaceContaining<RentCreatedConsumer>();

    // Define the endpoint name format to be prefixed by "search" and 
    // concatenated by hyphens, and with additional suffix from 
    // "Contracts" set to false
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

// Build the WebApplication using the services
var app = builder.Build();

// Configure the HTTP request pipeline.
// UseAuthorization() enables authorization for incoming requests
app.UseAuthorization();

// MapControllers() enables the routing for controllers
app.MapControllers();

// Register an action to be executed when the application starts
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        // Initialize the database and seed data into it
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
        // Log any exceptions that occur
        Console.WriteLine(e);
    }

});

// Start the application
app.Run();

// Use Microsoft Extensions Polly to handle transient HTTP errors
// HandleTransientHttpError() handles transient HTTP errors (e.g. 500 Internal Server Error)
// OrResult() adds an additional condition to handle HTTP 404 Not Found responses
// WaitAndRetryForeverAsync() retries the HTTP request forever with a delay of 3 seconds between each retry
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
