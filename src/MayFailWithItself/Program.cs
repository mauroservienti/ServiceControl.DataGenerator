using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("MayFailWithItself");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.DisableRecoverability();
        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();
        
        endpointConfiguration.OnEndpointStarted(session =>
        {
            return session.SendLocal(new ThisIsAMessage() { Behavior = MessageBehavior.Fail });
        });

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();