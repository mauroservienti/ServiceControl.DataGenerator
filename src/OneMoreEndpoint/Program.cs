using Microsoft.Extensions.Hosting;
using NServiceBus;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("OneMoreEndpoint");
        
        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        
        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();