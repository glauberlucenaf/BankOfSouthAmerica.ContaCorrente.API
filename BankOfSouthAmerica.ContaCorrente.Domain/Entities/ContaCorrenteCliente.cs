using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Domain.Entities
{
    public class ContaCorrenteCliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Conta { get; set; }
        public decimal SaldoTotal { get; set; }
    }
}
