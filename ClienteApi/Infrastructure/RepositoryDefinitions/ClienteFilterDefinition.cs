using MongoDB.Bson;
using MongoDB.Driver;
using TestesHttpClient.ClienteApi.Domain;
using System;
using System.Collections.Generic;

namespace TestesHttpClient.ClienteApi.Infrastructure.RepositoryDefinitions
{
    public class ClienteFilterDefinition
    {
        public static FilterDefinition<Cliente> Criar(string nomeParcial = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            List<FilterDefinition<Cliente>> campos = new List<FilterDefinition<Cliente>>
            {
                Builders<Cliente>.Filter.Eq(c => c.Tipo, 2)
            };
            if (!string.IsNullOrEmpty(nomeParcial))
            {
                campos.Add(Builders<Cliente>.Filter.Regex(c => c.Nome, new BsonRegularExpression($"/{nomeParcial}/")));
            }
            if (dataInicial.HasValue)
            {
                campos.Add(Builders<Cliente>.Filter.Gte(c => c.DataCadastro, dataInicial.Value));
            }
            if (dataFinal.HasValue)
            {
                campos.Add(Builders<Cliente>.Filter.Lte(c => c.DataCadastro, dataFinal.Value));
            }
            return Builders<Cliente>.Filter.And(campos);
        }
    }
}
