using Dapper;
using SEUtility.Common.Interfaces;
using SEUtility.Data.Interfaces;
using SEUtility.Data.Models;

namespace SEUtility.Data.Repositories;

internal class ItemRepository : IItemRepository
{
    private readonly IDatabase database;
    public ItemRepository(IDatabase database)
    {
        this.database = database;
    }
    public int Add(IEnumerable<Item> entities)
    {
        using var conn = database.GetConnection();
        var result = conn.Execute(DatabaseCommands.INSERT_ITEM, entities);

        return result;
    }

    public IReadOnlyList<Item> GetAll()
    {
        using var conn = database.GetConnection();
        var result = conn.Query<Item>(DatabaseCommands.SELECT_ITEMS).ToList();

        return result;
    }
}
