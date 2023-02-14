using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebJobServiceQueueStorage;

public class ProcessMessageFunction
{
    private readonly QueueClient _queueClient;

    public ProcessMessageFunction(IConfiguration configuration)
    {
        _queueClient = new QueueClient(configuration["AzureWebJobsStorage"], "processada");
    }

    public async Task ProcessQueueeMessage([QueueTrigger("minhafila")] string message, ILogger logger)
    {
        await _queueClient.SendMessageAsync($"Processed {message} {DateTime.Now}");
        logger.LogInformation(message);
    }
}