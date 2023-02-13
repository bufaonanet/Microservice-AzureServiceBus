using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddHostedService<WeatherForecastService>();

builder.Services.AddAzureClients(builder =>
{
    builder.AddClient<QueueClient, QueueClientOptions>((options, _, _) =>
    {
        options.MessageEncoding = QueueMessageEncoding.Base64;

        //var connectionString = "minha_conexao";
        //var queueName = "add-weatherdata";
        //return new QueueClient(connectionString, queueName, options);

        var credential = new DefaultAzureCredential();
        var queueUri = new Uri("https://minhacontadestorage.queue.core.windows.net/add-weatherdata");
        return new QueueClient(queueUri, credential, options);
    });
});

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