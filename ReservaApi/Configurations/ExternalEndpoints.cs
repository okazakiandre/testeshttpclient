using Microsoft.Extensions.Configuration;
using System;

namespace TestesHttpClient.ReservaApi.Configurations
{
    public class ExternalEndpoints : IExternalEndpoints
    {
        private IConfigurationSection ExtEnd { get; }

        public ExternalEndpoints(IConfiguration cfg)
        {
            ExtEnd = cfg.GetSection("externalEndpoints");
        }

        public ExternalEndpointItem GetItem(string endpointName)
        {
            var url = ExtEnd[$"{endpointName}:url"];
            var tmo = ExtEnd[$"{endpointName}:timeout"];
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException($"A seção \"externalEndpoints\" não contém a chave {endpointName}:url.");
            }
            if (string.IsNullOrEmpty(tmo))
            {
                throw new NullReferenceException($"A seção \"externalEndpoints\" não contém a chave {endpointName}:timeout.");
            }
            return new ExternalEndpointItem(url, int.Parse(tmo));
        }
    }
}
