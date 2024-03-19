namespace DataProviderApi.Models;

public interface IBooksManagementDatabaseSettings
{
    string CollectionName { get; set; }

    string ConnectionString { get; set; }

    string DatabaseName { get; set; }
}
