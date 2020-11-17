using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TestesHttpClient.ReservaApi.Domain;
using System;

namespace TestesHttpClient.ReservaApi.Infrastructure.Repositories
{
    public class ReservaDbMapper : BsonClassMap<Reserva>
    {
        public ReservaDbMapper()
        {
            AutoMap();
            SetIgnoreExtraElements(true);
            DateTimeSerializer dts = new DateTimeSerializer(DateTimeKind.Local);
            MapMember(r => r.DataEntrada).SetSerializer(dts);
            MapMember(r => r.DataSaida).SetSerializer(dts);
        }
    }
}
