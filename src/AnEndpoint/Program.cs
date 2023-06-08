using Messages;
using Microsoft.Extensions.Hosting;
using SharedConfig;

var numberOfConversationsToGenerate = 100;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpoint");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.DisableRecoverability();

        endpointConfiguration.OnEndpointStarted(session =>
        {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfConversationsToGenerate; i++)
            {
                tasks.Add(session.Send(new Kickoff()));
            }
            return Task.WhenAll(tasks);
        });

        var routingSettings = endpointConfiguration.ApplyCommonTransportConfiguration();
        routingSettings.RouteToEndpoint(typeof(Kickoff), "AnEndpointWithSagas");

        endpointConfiguration.ApplyCommonPlatformConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();