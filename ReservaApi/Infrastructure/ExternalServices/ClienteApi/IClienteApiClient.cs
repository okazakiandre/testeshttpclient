using System.Threading.Tasks;
using TestesHttpClient.ReservaApi.Domain;

namespace TestesHttpClient.ReservaApi.Infrastructure.ExternalServices.ClienteApi
{
    public interface IClienteApiClient
    {
        Task<Cliente> Obter(long cpfCnpj);
    }
}
