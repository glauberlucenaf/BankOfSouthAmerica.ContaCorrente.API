using BankOfSouthAmerica.ContaCorrente.Domain.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankOfSouthAmerica.ContaCorrente.Application.Services;
using BankOfSouthAmerica.ContaCorrente.Application.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Infra.Messaging;
using MongoDB.Driver;
using BankOfSouthAmerica.ContaCorrente.Infra.Settings;

namespace BankOfSouthAmerica.ContaCorrente.Infra
{
    public static class DependencyInjection
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteClienteRepository>();
            services.AddScoped<IContaCorrenteClienteService, ContaCorrenteClienteService>();
            services.AddSingleton<KafkaProducer>();

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var connectionString = configuration.GetSection("MongoDbSettings").GetSection("Server").Value; // ou sua string de conexão MongoDB
                return new MongoClient(connectionString);
            });

            Task.Run(() => services.AddHostedService<KafkaConsumer>());
        }
    }
}
