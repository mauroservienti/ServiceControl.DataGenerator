public static class CommonTransportConfiguration
{
    public static RoutingSettings ApplyCommonTransportConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        return endpointConfiguration.UseTransport(new RabbitMQTransport(RoutingTopology.Conventional(QueueType.Quorum), "host=localhost"));
    }
}