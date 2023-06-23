public static class CommonRecoverabilityConfiguration
{
    public static void DisableRecoverability(this EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration
            .Recoverability()
            .Immediate(i => i.NumberOfRetries(0))
            .Delayed(d => d.NumberOfRetries(0));
    }
}
