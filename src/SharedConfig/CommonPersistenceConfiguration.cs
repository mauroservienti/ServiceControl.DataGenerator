using Microsoft.Data.SqlClient;
using NServiceBus;

public static class CommonPersistenceConfiguration
{
    public static void ApplyCommonPersistenceConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        var connectionString = @"Server=.;Initial Catalog=ServiceControl.DataGenerator;User Id=SA;Password=YourStrongPassw0rd";
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.SqlDialect<SqlDialect.MsSqlServer>();
        persistence.ConnectionBuilder(() => new SqlConnection(connectionString));
    }
}