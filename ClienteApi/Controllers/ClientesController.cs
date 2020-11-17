using Microsoft.AspNetCore.Mvc;
using TestesHttpClient.ClienteApi.Domain;
using System;
using System.Threading.Tasks;

namespace TestesHttpClient.ClienteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private IClienteRepository CliRepo { get; }

        public ClientesController(IClienteRepository cli)
        {
            CliRepo = cli;
        }

        [HttpPost()]
        public async Task<IActionResult> Incluir([FromBody] Cliente cli)
        {
            var ret = await CliRepo.Incluir(cli);
            return Ok(new { sucesso = ret });
        }

        [HttpGet("{cpfCnpj}")]
        public async Task<IActionResult> Consultar(long cpfCnpj)
        {
            var ret = await CliRepo.Obter(cpfCnpj);
            return Ok(ret);
        }

        [HttpGet]
        public async Task<IActionResult> Consultar(string nomeParcial, DateTime? dataInicial, DateTime? dataFinal)
        {
            var ret = await CliRepo.Consultar(nomeParcial, dataInicial, dataFinal);
            return Ok(ret);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Cliente cli)
        {
            var ret = await CliRepo.Atualizar(cli);
            return Ok(new { sucesso = true, docsAlterados = ret });
        }
    }
}
