using MongoDB.Driver;

namespace TestesHttpClient.ClienteApi.Infrastructure.SeedWork
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
