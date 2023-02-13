using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace AzureFunctionConsumer
{
    public static class MyAzureFunction
    {
        [FunctionName("MyAzureFunction")]
        public static void Run([ServiceBusTrigger("product", Connection = "bus-connection")] string myQueueItem, ILogger log)
        {
            if (myQueueItem.Contains("exception"))
                throw new Exception();

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}