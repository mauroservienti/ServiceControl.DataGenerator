﻿using Microsoft.Extensions.Hosting;
using SharedConfig;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("OneMoreEndpoint");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.DisableRecoverability();

        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.ApplyCommonPlatformConfiguration();

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();