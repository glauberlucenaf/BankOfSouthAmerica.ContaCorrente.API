using BankOfSouthAmerica.ContaCorrente.Domain.Entities;
using BankOfSouthAmerica.ContaCorrente.Domain.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Infra.Messaging;
using BankOfSouthAmerica.ContaCorrente.Infra.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Repositories
{
    public class ContaCorrenteClienteRepository : IContaCorrenteRepository
    {
        private readonly KafkaProducer _producer;
        private readonly IMongoClient _mongoClient;
        private readonly CommonSettings _settings;

        public ContaCorrenteClienteRepository(KafkaProducer producer, IOptions<CommonSettings> settings, IMongoClient mongoClient)
        {
            _producer = producer;
            _mongoClient = mongoClient;
            _settings = settings.Value;
        }

        public async Task<ContaCorrenteCliente> CreateAsync(ContaCorrenteCliente contaCorrente)
        {
            _producer.ProduceAsync(JsonSerializer.Serialize(contaCorrente));

            return null;
        }

        public Task<ContaCorrenteCliente> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task IncludeDataBase(ContaCorrenteCliente contaConrrente)
        {
            var collection = _mongoClient.GetDatabase(_settings.MongoDbSettings.DataBase)
                .GetCollection<BsonDocument>(_settings.MongoDbSettings.CollectionContas);

            var document = new BsonDocument{
                                { "cpf", contaConrrente.Cpf },
                                { "nome", contaConrrente.Nome },
                                { "email", contaConrrente.Email },
                                { "conta", contaConrrente.Conta },
                                { "saldoTotal", contaConrrente.SaldoTotal }
                            };

            await collection.InsertOneAsync(document);
        }
    }
}
