using Dapper;
using SEUtility.Common.Interfaces;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;

namespace SEUtility.Data.Repositories;

internal class BlockRepository : IBlockRepository
{
    private readonly IDatabase database;
    public BlockRepository(IDatabase database)
    {
        this.database = database;
    }

    public int Add(IEnumerable<Block> entities)
    {
        using var conn = database.GetConnection();
        var result = conn.Execute(DatabaseCommands.INSERT_BLOCK, entities);

        return result;
    }

    public IReadOnlyList<Block> GetAll()
    {
        using var conn = database.GetConnection();
        var result = conn.Query<Block>(DatabaseCommands.SELECT_BLOCKS).ToList();

        return result;
    }
}
