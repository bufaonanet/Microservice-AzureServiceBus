using Azure.Storage.Queues.Models;
using System;

namespace FunctionQueueStorage.Servies
{
    public class MessageProcessor : IMessageProcessor
    {
        public void Process(string message)
        {
            if (message.Contains("exception"))
                throw new Exception("Exception found in message");
        }
    }
}
