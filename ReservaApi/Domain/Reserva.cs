using System;

namespace TestesHttpClient.ReservaApi.Domain
{
    public class Reserva
    {
        public int NumeroReserva { get; private set; }
        public long NumeroCpfCnpjCliente { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public short TipoQuarto { get; set; }

        public void GerarNumeroReserva()
        {
            NumeroReserva = GetHashCode();
        }
    }
}
