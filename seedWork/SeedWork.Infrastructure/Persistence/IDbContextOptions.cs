namespace SeedWork.Persistence;

public interface IDbContextOptions
{
    string DatabaseConnectionString { get; set; }
}
