using Messages;
using Microsoft.Extensions.Hosting;
using NServiceBus;

var numberOfConversationsToGenerate = 100;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnotherEndpoint");
        
        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        
        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();