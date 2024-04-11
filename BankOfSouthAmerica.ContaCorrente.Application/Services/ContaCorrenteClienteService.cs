using BankOfSouthAmerica.ContaCorrente.Application.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Domain.Entities;
using BankOfSouthAmerica.ContaCorrente.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Application.Services
{
    public class ContaCorrenteClienteService : IContaCorrenteClienteService
    {
        private readonly IContaCorrenteRepository _repository;

        public ContaCorrenteClienteService(IContaCorrenteRepository repository)
        {
            _repository = repository;   
        }
        public async Task<ContaCorrenteCliente> CriaContaCorrenteKafka(ContaCorrenteCliente contaCorrente)
        {
            return await _repository.CreateAsync(contaCorrente);
        }
    }
}
