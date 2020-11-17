using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestesHttpClient.ReservaApi.Domain
{
    public interface IReservaRepository
    {
        Task<bool> Incluir(Reserva rsv);
        Task<Reserva> Obter(int numeroReserva);
        Task<List<Reserva>> ObterLista(long cpfCnpj);
    }
}