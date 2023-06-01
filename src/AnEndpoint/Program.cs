using Messages;
using Microsoft.Extensions.Hosting;
using NServiceBus;

var numberOfConversationsToGenerate = 1000;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpoint");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.OnEndpointStarted(session => 
        {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfConversationsToGenerate; i++)
            {
                tasks.Add(session.Send(new Kickoff()));
            }
            return Task.WhenAll(tasks);
        });

        var routingSettings = endpointConfiguration.UseTransport(new RabbitMQTransport(RoutingTopology.Conventional(QueueType.Quorum), "host=localhost"));
        routingSettings.RouteToEndpoint(typeof(Kickoff), "AnEndpointWithSagas");

        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();