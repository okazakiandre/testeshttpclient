using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestesHttpClient.ReservaApi.Domain;
using TestesHttpClient.ReservaApi.Infrastructure.ExternalServices.ClienteApi;

namespace TestesHttpClient.ReservaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservasController : ControllerBase
    {
        private IReservaRepository RsvRepo { get; }
        private IClienteApiClient CliApi { get; }
        public ReservasController(IReservaRepository repo, IClienteApiClient cli)
        {
            RsvRepo = repo;
            CliApi = cli;
        }

        [HttpGet("{numeroReserva}")]
        public async Task<IActionResult> Obter(int numeroReserva)
        {
            var rsv = await RsvRepo.Obter(numeroReserva);
            return Ok(rsv);
        }

        [HttpGet]
        public async Task<IActionResult> ObterLista(long cpfCnpj)
        {
            var lista = await RsvRepo.ObterLista(cpfCnpj);
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Solicitar(SolicitacaoReservaDto solicitacao)
        {
            var cliente = await CliApi.Obter(solicitacao.NumeroCpfCnpjCliente);
            if (cliente is null)
            {
                return Ok("Cliente não encontrado, por favor efetue o cadastro antes de solicitar uma reserva.");
            }

            if (!cliente.PodeReservar(solicitacao.DataEntrada))
            {
                return Ok("Não é possível efetuar a reserva para a data informada.");
            }

            var rsv = new Reserva
            {
                NumeroCpfCnpjCliente = solicitacao.NumeroCpfCnpjCliente,
                DataEntrada = solicitacao.DataEntrada,
                DataSaida = solicitacao.DataSaida,
                TipoQuarto = solicitacao.TipoQuarto
            };
            await RsvRepo.Incluir(rsv);

            return Ok($"Reserva número {rsv.NumeroReserva} confirmada.");
        }

    }
}
