using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestesHttpClient.ReservaApi.Domain;

namespace TestesHttpClient.ReservaApi.Infrastructure.ExternalServices.ClienteApi
{
    public class ClienteApiClient : IClienteApiClient
    {
        private HttpClient ApiCli { get; }
        public ClienteApiClient(HttpClient cli)
        {
            ApiCli = cli;
        }

        public async Task<Cliente> Obter(long cpfCnpj)
        {
            var resp = await ApiCli.GetAsync($"clientes/{cpfCnpj}");
            resp.EnsureSuccessStatusCode();
            var respJson = await resp.Content.ReadAsStringAsync();
            Cliente cliente = null;
            if (!string.IsNullOrEmpty(respJson))
            {
                cliente = JsonSerializer.Deserialize<Cliente>(respJson, new JsonSerializerOptions { AllowTrailingCommas = true, PropertyNameCaseInsensitive = true });
            }
            return cliente;
        }
    }
}
