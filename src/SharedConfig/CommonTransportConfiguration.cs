using NServiceBus;
using NServiceBus.Transport;

public static class CommonTransportConfiguration
{
    public static TransportDefinition GetTransportDefinition()
    {
        var connectionString = @"Server=.;Initial Catalog=ServiceControl.DataGenerator;User Id=SA;Password=YourStrongPassw0rd";
        return new SqlServerTransport(connectionString)
        {
            TransportTransactionMode = TransportTransactionMode.ReceiveOnly
        };
    }
    
    public static RoutingSettings ApplyCommonTransportConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        //return endpointConfiguration.UseTransport(new LearningTransport());
        return endpointConfiguration.UseTransport(GetTransportDefinition());
    }
}