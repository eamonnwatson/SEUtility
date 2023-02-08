using System.Data;

namespace SEUtility.Common.Interfaces;

public interface IDatabase
{
    public string Location { get; }
    IDbConnection GetConnection();
    void SetLocation(string location);
    void EnsureCreated();
}
