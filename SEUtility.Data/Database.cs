using Dapper;
using Microsoft.Data.Sqlite;
using SEUtility.Common.Interfaces;
using System.Data;

namespace SEUtility.Data;

internal class Database : IDatabase
{
    private const string DATABASE_NAME = "space_engineers.db";
    private string databaseLocation = Path.Combine(Environment.CurrentDirectory, DATABASE_NAME);
    public string Location { get => databaseLocation; }
    public void EnsureCreated()
    {
        using var conn = GetConnection();
        conn.Execute(DatabaseCommands.CREATE_ITEM_TABLE);
        conn.Execute(DatabaseCommands.CREATE_BLOCK_TABLE);
        conn.Execute(DatabaseCommands.CREATE_BLUEPRINT_TABLE);
        conn.Execute(DatabaseCommands.CREATE_BLUEPRINTCLASS_TABLE);
        conn.Execute(DatabaseCommands.CREATE_BLOCKCATEGORY_TABLE);
    }

    public IDbConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={databaseLocation};");
    }

    public void SetLocation(string location)
    {
        databaseLocation = Path.Combine(location, DATABASE_NAME);
    }
}
