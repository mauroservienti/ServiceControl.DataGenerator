using Microsoft.Extensions.Hosting;
using NServiceBus;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("OneMoreEndpoint");
        endpointConfiguration.EnableInstallers();
        
        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();