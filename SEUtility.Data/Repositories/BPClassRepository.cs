using Dapper;
using SEUtility.Common.Interfaces;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;

namespace SEUtility.Data.Repositories;

internal class BPClassRepository : IBPClassRepository
{
    private readonly IDatabase database;
    public BPClassRepository(IDatabase database)
    {
        this.database = database;
    }

    public int Add(IEnumerable<BlueprintClass> entities)
    {
        using var conn = database.GetConnection();
        var result = conn.Execute(DatabaseCommands.INSERT_BLUEPRINTCLASS, entities);

        return result;
    }

    public IReadOnlyList<BlueprintClass> GetAll()
    {
        using var conn = database.GetConnection();
        var result = conn.Query<BlueprintClass>(DatabaseCommands.SELECT_BLUEPRINTCLASS).ToList();

        return result;
    }
}
