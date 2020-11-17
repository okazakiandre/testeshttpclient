using System;

namespace TestesHttpClient.ReservaApi.Controllers
{
    public class SolicitacaoReservaDto
    {
        public long NumeroCpfCnpjCliente { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public short TipoQuarto { get; set; }
        public string Observacoes { get; set; }
    }
}
