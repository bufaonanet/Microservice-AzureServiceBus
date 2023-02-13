using Azure.Messaging.ServiceBus;

namespace ApiProducer.HostedServices;

public class ProductQueueConsumer : BackgroundService
{
    private readonly ILogger<ProductQueueConsumer> _logger;
    private readonly ServiceBusClient _client;
    public const string queueName = "product";

    public ProductQueueConsumer(
        ILogger<ProductQueueConsumer> logger, 
        ServiceBusClient client)
    {
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("########### Starting Consumer - Queue ########### ");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}.", DateTimeOffset.Now);
            await ProcessMessageHandler();
        }

        _logger.LogInformation("########### Stopping Consumer - Queue ########### ");
    }

    private async Task ProcessMessageHandler()
    {
        // create a receiver that we can use to receive the message
        ServiceBusReceiver receiver = _client.CreateReceiver(queueName);

        // the received message is a different type as it contains some service set properties
        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();        

        // get the message body as a string
        string body = receivedMessage.Body.ToString();

       // _logger.LogInformation(body);
        Console.WriteLine(body);

        // complete the message, thereby deleting it from the service
        await receiver.CompleteMessageAsync(receivedMessage);
    }
}