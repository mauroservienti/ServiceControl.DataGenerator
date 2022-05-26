using Messages;
using Microsoft.Extensions.Hosting;
using NServiceBus;

var numberOfConversationsToGenerate = 100;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpoint");
        endpointConfiguration.OnEndpointStarted(session => 
        {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfConversationsToGenerate; i++)
            {
                tasks.Add(session.Send(new Kickoff()));
            }
            return Task.WhenAll(tasks);
        });

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.Routing()
            .RouteToEndpoint(typeof(Kickoff), "AnEndpointWithSagas");

        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        
        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();