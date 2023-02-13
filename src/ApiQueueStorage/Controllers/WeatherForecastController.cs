using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiQueueStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly QueueClient _queueClient;

        public WeatherForecastController(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        [HttpPost]
        public async Task Post([FromBody] WeatherForecast data)
        {            
            var message = JsonSerializer.Serialize(data);
            await _queueClient.SendMessageAsync(message, null, TimeSpan.FromSeconds(-1));
        }
    }
}