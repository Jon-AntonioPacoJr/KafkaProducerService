using Confluent.Kafka;

namespace KafkaProducerService.Services
{
    public class KafkaProducerService1
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducerService1(IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            _topic = configuration["Kafka:Topic"];
        }

        public async Task ProduceMessageAsync(string message)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
