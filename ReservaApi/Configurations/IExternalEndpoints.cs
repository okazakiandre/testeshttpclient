namespace TestesHttpClient.ReservaApi.Configurations
{
    public interface IExternalEndpoints
    {
        ExternalEndpointItem GetItem(string endpointName);
    }
}
