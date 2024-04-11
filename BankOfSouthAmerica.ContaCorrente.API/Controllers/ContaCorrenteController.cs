using BankOfSouthAmerica.ContaCorrente.Application.Interfaces;
using BankOfSouthAmerica.ContaCorrente.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankOfSouthAmerica.ContaCorrente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteClienteService _service;

        public ContaCorrenteController(IContaCorrenteClienteService service)
        {
            _service = service;
        }

        [HttpPost(Name = "CriaContaCorrente")]
        public async Task<IActionResult> CriaContaCliente(ContaCorrenteCliente contaCorrente)
        {
            return Ok(await _service.CriaContaCorrenteKafka(contaCorrente));
        }
    }
}
