using HealthService.Infrastructure; 
using HealthService.Infrastructure.EventsBus;
using HealthService.Domain.Events;
using HealthService.Application.Events.Handlers;
using HealthService.Api.Contracts;
using HealthService.Application;
using HealthService.Api.Storage;


var builder = WebApplication.CreateBuilder(args);

// CORS — SIEMPRE ANTES DE app.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// -----------------------------------------
// REGISTRO DE SERVICIOS
// -----------------------------------------
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();



var app = builder.Build();

// -----------------------------------------
// MIDDLEWARES
// -----------------------------------------
app.UseCors("AllowFrontend");

// -----------------------------------------
// EVENT BUS
// -----------------------------------------
var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<MessagePublishedEvent>(async (evt) =>
{
    var handler = new MessagePublishedEventHandler();
    await handler.HandleAsync(evt);
});

// -----------------------------------------
// ENDPOINTS
// -----------------------------------------
app.MapPost("/publish", async (PublishRequest req, IEventBus bus) =>
{
    var evt = new MessagePublishedEvent(Guid.NewGuid(), DateTime.UtcNow, req.Message);
    await bus.PublishAsync(evt);

    // Guardar el último mensaje
    MessageStore.LastMessage = req.Message;

    return Results.Ok(new
    {
        published = true,
        evt.Id,
        evt.Message
    });
});
app.MapGet("/last", () =>
{
    return Results.Json(new
    {
        message = MessageStore.LastMessage
    });
});


app.MapGet("/health", () => Results.Json(new { status = "Eche" }));

// -----------------------------------------
app.Run();