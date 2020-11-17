using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TestesHttpClient.ClienteApi.Domain;
using System;

namespace TestesHttpClient.ClienteApi.Infrastructure.Repositories
{
    public class ClienteDbMapper : BsonClassMap<Cliente>
    {
        public ClienteDbMapper()
        {
            AutoMap();
            SetIgnoreExtraElements(true);
            DateTimeSerializer dts = new DateTimeSerializer(DateTimeKind.Local);
            MapMember(c => c.DataCadastro).SetSerializer(dts);
            MapMember(c => c.DataNascimento).SetSerializer(dts);
        }
    }
}
