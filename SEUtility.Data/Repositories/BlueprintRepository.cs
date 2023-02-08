using Dapper;
using SEUtility.Common.Interfaces;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;

namespace SEUtility.Data.Repositories;

internal class BlueprintRepository : IBlueprintRepository
{
    private readonly IDatabase database;
    public BlueprintRepository(IDatabase database)
    {
        this.database = database;
    }

    public int Add(IEnumerable<Blueprint> entities)
    {
        using var conn = database.GetConnection();
        var result = conn.Execute(DatabaseCommands.INSERT_BLUEPRINT, entities);

        return result;
    }


    public IReadOnlyList<Blueprint> GetAll()
    {
        using var conn = database.GetConnection();
        var result = conn.Query<Blueprint>(DatabaseCommands.SELECT_BLUEPRINTS).ToList();

        return result;
    }
}
