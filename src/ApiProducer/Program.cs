using ApiProducer.HostedServices;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAzureClients(options =>
{
    //options.AddServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"));

    options.AddClient<ServiceBusClient, ServiceBusClientOptions>((_, _, _) =>
    {
        return new ServiceBusClient("bufa-bus.servicebus.windows.net", new DefaultAzureCredential());
    });
});

//builder.Services.AddHostedService<ProductQueueConsumer>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
