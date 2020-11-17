using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestesHttpClient.ReservaApi.Domain;

namespace TestesHttpClient.UnitTest.Domain
{
    [TestClass]
    [TestCategory("UnitTest > ReservaApi > Domain")]
    public class ClienteTest
    {
        [TestMethod]
        public void NaoDeveriaPermitirReservaParaClienteVipCom0DiasDeAntecedencia()
        {
            var cli = new Cliente { Tipo = 1 };
            var res = cli.PodeReservar(DateTime.Today);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void DeveriaPermitirReservaParaClienteVipCom1DiaDeAntecedencia()
        {
            var cli = new Cliente { Tipo = 1 };
            var res = cli.PodeReservar(DateTime.Today.AddDays(1));
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void NaoDeveriaPermitirReservaParaClienteNaoVipCom9DiasDeAntecedencia()
        {
            var cli = new Cliente { Tipo = 2 };
            var res = cli.PodeReservar(DateTime.Today.AddDays(9));
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void DeveriaPermitirReservaParaClienteVipCom10DiaDeAntecedencia()
        {
            var cli = new Cliente { Tipo = 2 };
            var res = cli.PodeReservar(DateTime.Today.AddDays(10));
            Assert.IsTrue(res);
        }
    }
}
