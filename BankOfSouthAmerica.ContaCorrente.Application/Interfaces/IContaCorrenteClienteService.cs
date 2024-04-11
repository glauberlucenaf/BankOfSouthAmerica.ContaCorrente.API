using BankOfSouthAmerica.ContaCorrente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Application.Interfaces
{
    public interface IContaCorrenteClienteService
    {
        public Task<ContaCorrenteCliente> CriaContaCorrenteKafka(ContaCorrenteCliente contaCorrente);
    }
}
