public static class PlatfromConnectionConfiguration
{
    public static void ApplyCommonPlatformConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
        endpointConfiguration.ReportCustomChecksTo("Particular.ServiceControl");
        endpointConfiguration.AuditSagaStateChanges("Particular.ServiceControl");
    }
}
