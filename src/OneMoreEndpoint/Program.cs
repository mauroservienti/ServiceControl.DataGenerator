using Microsoft.Extensions.Hosting;
using NServiceBus;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("OneMoreEndpoint");
        endpointConfiguration.EnableInstallers();
        
        endpointConfiguration.ApplyCommonTransportConfiguration();
        
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();