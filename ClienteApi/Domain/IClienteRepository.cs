using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestesHttpClient.ClienteApi.Domain
{
    public interface IClienteRepository
    {
        Task<bool> Incluir(Cliente cli);
        Task<long> Atualizar(Cliente cli);
        Task<Cliente> Obter(long cpfCnpj);
        Task<List<Cliente>> Consultar(string nomeParcial = null, DateTime? dataInicial = null, DateTime? dataFinal = null);
    }
}
