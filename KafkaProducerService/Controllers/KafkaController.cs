using KafkaProducerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly KafkaProducerService1 _kafkaProducerService;

        public KafkaController(KafkaProducerService1 kafkaProducerService)
        {
            _kafkaProducerService = kafkaProducerService;
        }

        [HttpPost]
        [Route("produce")]
        public async Task<IActionResult> Produce([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be null or empty");
            }

            await _kafkaProducerService.ProduceMessageAsync(message);
            return Ok("Message sent to Kafka");
        }
    }
}
