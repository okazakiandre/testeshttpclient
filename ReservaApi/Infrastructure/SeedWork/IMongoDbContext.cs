using MongoDB.Driver;

namespace TestesHttpClient.ReservaApi.Infrastructure.SeedWork
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
