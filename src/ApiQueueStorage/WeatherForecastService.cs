using ApiQueueStorage.Controllers;
using Azure.Storage.Queues;
using System.Text.Json;

namespace ApiQueueStorage;

public class WeatherForecastService : BackgroundService
{
    private readonly ILogger<WeatherForecastService> _logger;
    private readonly QueueClient _queueClient;

    public WeatherForecastService(ILogger<WeatherForecastService> logger, QueueClient queueClient)
    {
        _logger = logger;
        _queueClient = queueClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Reading from queue");
            var queueMessage = await _queueClient.ReceiveMessageAsync();

            if (queueMessage.Value != null)
            {
                var whatherData = JsonSerializer.Deserialize<WeatherForecast>(queueMessage.Value.Body);
                _logger.LogInformation("New message Read :{whatherData}", whatherData.Summary);

                //application process
                await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);
            }
            else
            {
                _logger.LogInformation("No messages in the Queue!");
            }

            await Task.Delay(TimeSpan.FromSeconds(10));  
        }
    }
}
