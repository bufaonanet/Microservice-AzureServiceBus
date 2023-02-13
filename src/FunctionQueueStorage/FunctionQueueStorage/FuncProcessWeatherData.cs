using FunctionQueueStorage.Servies;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FunctionQueueStorage
{
    public class FuncProcessWeatherData
    {
        private readonly IMessageProcessor _messageProcessor;
        private readonly ILogger<FuncProcessWeatherData> _logger;
        private readonly IOptions<MyConfigOptions> _options;

        public FuncProcessWeatherData(
            IMessageProcessor messageProcessor,
            ILogger<FuncProcessWeatherData> logger,
            IOptions<MyConfigOptions> options)
        {
            _messageProcessor = messageProcessor;
            _logger = logger;
            _options = options;
        }

        [FunctionName("ProcessWeatherData")]
        public void Run([QueueTrigger("add-weatherdata", Connection = "WeatherDataQueue")] string myQueueItem)
        {
            _messageProcessor.Process(myQueueItem);

            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
