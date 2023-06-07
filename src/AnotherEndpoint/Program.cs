using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnotherEndpoint");
        endpointConfiguration.EnableInstallers();
        
        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();