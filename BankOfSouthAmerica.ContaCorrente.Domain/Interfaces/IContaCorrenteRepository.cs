using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfSouthAmerica.ContaCorrente.Domain.Entities;

namespace BankOfSouthAmerica.ContaCorrente.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        public Task<ContaCorrenteCliente> CreateAsync(ContaCorrenteCliente contaCorrente);
        public Task<ContaCorrenteCliente> GetByCPF(string cpf);
        public Task IncludeDataBase(ContaCorrenteCliente);
    }
}
