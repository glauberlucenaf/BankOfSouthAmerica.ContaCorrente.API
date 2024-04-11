using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Text.Json;
using BankOfSouthAmerica.ContaCorrente.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using BankOfSouthAmerica.ContaCorrente.Infra.Settings;
using Microsoft.Extensions.Options;
using BankOfSouthAmerica.ContaCorrente.Domain.Interfaces;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Messaging
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ConsumerConfig _config;
        private readonly IMongoClient _mongoClient;
        private readonly CommonSettings _settings;
        private readonly IContaCorrenteRepository _repository;

        public KafkaConsumer(IMongoClient mongoClient, IOptions<CommonSettings> settings)
        {
            _settings = settings.Value;
            _config = new ConsumerConfig
            {
                BootstrapServers = _settings.KafkaSettings.BootstrapServer,
                GroupId = "my-consumer-group", // Identificador do grupo de consumidores
                AutoOffsetReset = AutoOffsetReset.Earliest // Configuração do offset inicial
            };
            _mongoClient = mongoClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_settings.KafkaSettings.TopicName);
            var collection = _mongoClient.GetDatabase(_settings.MongoDbSettings.DataBase)
                .GetCollection<BsonDocument>(_settings.MongoDbSettings.CollectionContas);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        var message = consumeResult.Message.Value;

                        if (!string.IsNullOrEmpty(message))
                            _repository.IncludeDataBase(JsonSerializer.Deserialize<ContaCorrenteCliente>(message));

                        // Processar a mensagem conforme necessário
                        //Console.WriteLine($"Mensagem recebida do Kafka: {message}");

                        // Confirmar o offset da mensagem para indicar que foi processada com sucesso
                        consumer.Commit(consumeResult);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Erro ao consumir a mensagem: {e.Error}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //Console.WriteLine($"Erro ao consumir a mensagem");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
