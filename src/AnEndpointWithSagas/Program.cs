﻿using Microsoft.Extensions.Hosting;
using NServiceBus;

var host = Host.CreateDefaultBuilder()
    .UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("AnEndpointWithSagas");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.AuditSagaStateChanges("Particular.ServiceControl");

        endpointConfiguration
            .Recoverability()
                .Immediate(i => i.NumberOfRetries(0))
                .Delayed(d => d.NumberOfRetries(0));

        endpointConfiguration.ApplyCommonTransportConfiguration();
        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

        return endpointConfiguration;
    })
    .Build();

await host.RunAsync();