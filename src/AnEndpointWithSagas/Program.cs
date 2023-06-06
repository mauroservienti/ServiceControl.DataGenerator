using Microsoft.Extensions.Hosting;
using NServiceBus;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpointWithSagas");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration
            .Recoverability()
                .Immediate(i => i.NumberOfRetries(0))
                .Delayed(d => d.NumberOfRetries(0));

        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        endpointConfiguration.UsePersistence<LearningPersistence>();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();