using BankOfSouthAmerica.ContaCorrente.Application.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Application.Services;
using BankOfSouthAmerica.ContaCorrente.Domain.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Infra;
using BankOfSouthAmerica.ContaCorrente.Infra.Messaging;
using BankOfSouthAmerica.ContaCorrente.Infra.Repositories;
using BankOfSouthAmerica.ContaCorrente.Infra.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CommonSettings>(builder.Configuration);

builder.Services.AddInfra(builder.Configuration);

//Task.Run(() => builder.Services.AddHostedService(provider =>
//{
//    return new KafkaConsumer("localhost:9092", "bank-of-southamerica-topic", new MongoClient("mongodb://localhost:27017"));
//}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
