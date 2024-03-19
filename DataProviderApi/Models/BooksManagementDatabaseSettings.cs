namespace DataProviderApi.Models;

public class BooksManagementDatabaseSettings : IBooksManagementDatabaseSettings
{
    public string CollectionName { get; set; } = string.Empty;

    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;
}
