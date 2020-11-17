using MongoDB.Driver;
using TestesHttpClient.ClienteApi.Domain;
using TestesHttpClient.ClienteApi.Infrastructure.RepositoryDefinitions;
using TestesHttpClient.ClienteApi.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestesHttpClient.ClienteApi.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private IMongoCollection<Cliente> CliCol { get; }

        public ClienteRepository(IMongoDbContext db)
        {
            CliCol = db.GetCollection<Cliente>("Cliente");
        }

        public async Task<bool> Incluir(Cliente cli)
        {
            await CliCol.InsertOneAsync(cli);
            return true;
        }

        public async Task<long> Atualizar(Cliente cli)
        {
            var updDef = ClienteUpdateDefinition.Criar(cli);
            var res = await CliCol.UpdateOneAsync(c => c.NumeroCpfCnpj == cli.NumeroCpfCnpj, updDef);
            return res.ModifiedCount;
        }

        public async Task<Cliente> Obter(long cpfCnpj)
        {
            return await CliCol.Find(c => c.NumeroCpfCnpj == cpfCnpj).SingleOrDefaultAsync();
        }

        public async Task<List<Cliente>> Consultar(string nomeParcial = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            var filtro = ClienteFilterDefinition.Criar(nomeParcial, dataInicial, dataFinal);
            return await CliCol.Find(filtro).ToListAsync();
        }
    }
}
