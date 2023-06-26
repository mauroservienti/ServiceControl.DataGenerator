using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpointWithSagas");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.DisableRecoverability();

        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        endpointConfiguration.ApplyCommonPersistenceConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();