using BankOfSouthAmerica.ContaCorrente.Infra.Settings;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Messaging
{
    public class KafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly CommonSettings _settings;

        public KafkaProducer(IOptions<CommonSettings> settings)
        {
            _settings = settings.Value;
            _config = new ProducerConfig { BootstrapServers = _settings.KafkaSettings.BootstrapServer };
        }

        public async Task ProduceAsync<T>(T message)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                try
                {
                    var serializedMessage = message.ToString(); // Converte o objeto para uma string
                    var deliveryResult = await producer.ProduceAsync(_settings.KafkaSettings.TopicName, new Message<Null, string> { Value = serializedMessage });

                    Console.WriteLine($"Mensagem enviada para o Kafka. Tópico: {deliveryResult.Topic}, Partição: {deliveryResult.Partition}, Offset: {deliveryResult.Offset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Erro ao produzir a mensagem: {e.Error.Reason}");
                    // Aqui você pode lidar com o erro conforme necessário
                }
            }
        }
    }
}
