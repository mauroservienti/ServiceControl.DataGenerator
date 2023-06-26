public static class CommonPersistenceConfiguration
{
    public static void ApplyCommonPersistenceConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.UsePersistence<LearningPersistence>();
    }
}