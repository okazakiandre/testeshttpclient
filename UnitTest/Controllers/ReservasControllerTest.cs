using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestesHttpClient.ReservaApi.Controllers;
using TestesHttpClient.ReservaApi.Domain;
using TestesHttpClient.ReservaApi.Infrastructure.ExternalServices.ClienteApi;

namespace TestesHttpClient.UnitTest.Controllers
{
    [TestClass]
    [TestCategory("UnitTest > ReservaApi > Controllers")]
    public class ReservasControllerTest
    {
        [TestMethod]
        public async Task DeveriaGerarErroNaReservaParaUmCpfCnpjNaoCadastrado()
        {
            var mockRep = new Mock<IReservaRepository>();
            var mockCli = new Mock<IClienteApiClient>();
            var ctr = new ReservasController(mockRep.Object, mockCli.Object);

            var res = await ctr.Solicitar(new SolicitacaoReservaDto { NumeroCpfCnpjCliente = 0 }) as OkObjectResult;

            Assert.AreEqual("Cliente não encontrado, por favor efetue o cadastro antes de solicitar uma reserva.", res.Value);
        }

        [TestMethod]
        public async Task DeveriaGerarErroNaReservaSeOClienteNaoPodeReservarNaData()
        {
            var mockRep = new Mock<IReservaRepository>();
            var mockCli = new Mock<IClienteApiClient>();
            mockCli.Setup(m => m.Obter(It.IsAny<long>())).ReturnsAsync(new Cliente { Tipo = 1 });
            var ctr = new ReservasController(mockRep.Object, mockCli.Object);
            var req = new SolicitacaoReservaDto { DataEntrada = DateTime.Today };

            var res = await ctr.Solicitar(req) as OkObjectResult;

            Assert.AreEqual("Não é possível efetuar a reserva para a data informada.", res.Value);
        }

        [TestMethod]
        public async Task DeveriaConcluirAReservaSeOClientePodeReservarNaData()
        {
            var mockRep = new Mock<IReservaRepository>();
            var mockCli = new Mock<IClienteApiClient>();
            mockCli.Setup(m => m.Obter(It.IsAny<long>())).ReturnsAsync(new Cliente { Tipo = 1 });
            var ctr = new ReservasController(mockRep.Object, mockCli.Object);
            var req = new SolicitacaoReservaDto { DataEntrada = DateTime.Today.AddDays(1) };

            var res = await ctr.Solicitar(req) as OkObjectResult;

            Assert.AreEqual("Reserva número 0 confirmada.", res.Value);
        }
    }
}
