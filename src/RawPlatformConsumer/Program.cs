using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Transport;

var host = Host.CreateDefaultBuilder()
    .Build();

var transportDefinition = CommonTransportConfiguration.GetTransportDefinition();

var hostSettings = new HostSettings(
    "raw-platform",
    "RawPlatformConsumer",
    new StartupDiagnosticEntries(),
    (s, exception, cancellationToken) =>
    {
        throw exception;
    },
    true);
var receivers = new ReceiveSettings[]
{
    new (
        "error-receiver",
        new QueueAddress("error"), 
        false, 
        false, 
        "raw-platform-error"),
    new (
        "audit-receiver",
        new QueueAddress("audit"),
        false,
        false,
        "raw-platform-error"),
    new (
        "Particular.ServiceControl-receiver",
        new QueueAddress("Particular.ServiceControl"),
        false,
        false,
        "raw-platform-error"),
};
var sendingAddresses = new[] { "Particular.ServiceControl" };

var transportInfrastructure = await transportDefinition.Initialize(hostSettings, receivers, sendingAddresses);

Debug.Assert(transportInfrastructure?.Receivers != null, "transportInfrastructure?.Receivers != null");
foreach (var (key, messageReceiver) in transportInfrastructure.Receivers)
{
    await messageReceiver.Initialize(
        new PushRuntimeSettings(5),
        (context, token) =>
        {
            return Task.CompletedTask;
        },
        (context, token) =>
        {
            return Task.FromResult(ErrorHandleResult.Handled);
        })!;
    
    await messageReceiver.StartReceive()!;
}

await host.RunAsync();

foreach (var (key, messageReceiver) in transportInfrastructure.Receivers)
{
    await messageReceiver.StopReceive();
}

await transportInfrastructure.Shutdown();