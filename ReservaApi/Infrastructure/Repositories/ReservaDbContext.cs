using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TestesHttpClient.ReservaApi.Domain;
using TestesHttpClient.ReservaApi.Infrastructure.SeedWork;

namespace TestesHttpClient.ReservaApi.Infrastructure.Repositories
{
    public class ReservaDbContext : MongoDbContextBase, IMongoDbContext
    {
        public ReservaDbContext(IConfiguration config) : base(config)
        {
            OnRegisterMappers();
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        protected override void OnRegisterMappers()
        {
            RegisterClassMap<Reserva, ReservaDbMapper>();
        }

    }
}
