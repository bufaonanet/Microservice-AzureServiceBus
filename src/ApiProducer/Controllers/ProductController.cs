using ApiProducer.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ServiceBusClient _client;
        public const string queueName = "product";

        public ProductController(ServiceBusClient client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await SendMessageQueue(product);
            return Ok(product);
        }

        private async Task SendMessageQueue(Product product)
        {
            //serialize the object
            var productJson = JsonSerializer.Serialize(product);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new(productJson);


            if (productJson.Contains("scheduled"))
                message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(10);

            if (productJson.Contains("ttl"))
                message.TimeToLive = TimeSpan.FromSeconds(20);

            // create the sender
            ServiceBusSender sender = _client.CreateSender(queueName);

            // send the message
            await sender.SendMessageAsync(message);
        }
    }
}
