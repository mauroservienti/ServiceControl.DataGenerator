using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("MayFailWithItself");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration
            .Recoverability()
                .Immediate(i => i.NumberOfRetries(0))
                .Delayed(d => d.NumberOfRetries(0));

        endpointConfiguration.OnEndpointStarted(session =>
        {
            return session.SendLocal(new ThisIsAMessage() { Behavior = MessageBehavior.Fail });
        });

        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();