using Dapper;
using SEUtility.Common.Interfaces;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;

namespace SEUtility.Data.Repositories;

internal class BlockCategoryRepository : IBlockCategoryRepository
{
    private readonly IDatabase database;
    public BlockCategoryRepository(IDatabase database)
    {
        this.database = database;
    }

    public int Add(IEnumerable<BlockCategory> entities)
    {
        using var conn = database.GetConnection();
        var result = conn.Execute(DatabaseCommands.INSERT_BLOCKCATEGORY, entities);

        return result;
    }

    public IReadOnlyList<BlockCategory> GetAll()
    {
        using var conn = database.GetConnection();
        var result = conn.Query<BlockCategory>(DatabaseCommands.SELECT_BLOCKCATEGORY).ToList();

        return result;
    }
}
