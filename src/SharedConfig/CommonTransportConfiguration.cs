using NServiceBus;
using NServiceBus.Transport;

public static class CommonTransportConfiguration
{
    public static TransportDefinition GetTransportDefinition()
    {
        return new RabbitMQTransport(RoutingTopology.Conventional(QueueType.Quorum), "host=localhost")
        {
            TransportTransactionMode = TransportTransactionMode.TransactionScope
        };
    }
    
    public static RoutingSettings ApplyCommonTransportConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        //return endpointConfiguration.UseTransport(new LearningTransport());
        return endpointConfiguration.UseTransport(GetTransportDefinition());
    }
}