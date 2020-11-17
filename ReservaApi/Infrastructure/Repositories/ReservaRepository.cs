using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestesHttpClient.ReservaApi.Domain;
using TestesHttpClient.ReservaApi.Infrastructure.SeedWork;

namespace TestesHttpClient.ReservaApi.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private IMongoCollection<Reserva> RsvCol { get; }

        public ReservaRepository(IMongoDbContext db)
        {
            RsvCol = db.GetCollection<Reserva>("Reserva");
        }

        public async Task<bool> Incluir(Reserva rsv)
        {
            rsv.GerarNumeroReserva();
            await RsvCol.InsertOneAsync(rsv);
            return true;
        }

        public async Task<Reserva> Obter(int numeroReserva)
        {
            return await RsvCol.Find(r => r.NumeroReserva == numeroReserva).SingleOrDefaultAsync();
        }

        public async Task<List<Reserva>> ObterLista(long cpfCnpj)
        {
            return await RsvCol.Find(r => r.NumeroCpfCnpjCliente == cpfCnpj).ToListAsync();
        }
    }
}
