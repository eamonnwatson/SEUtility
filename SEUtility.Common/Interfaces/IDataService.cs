using SEUtility.Common.Models;

namespace SEUtility.Common.Interfaces;

public interface IDataService
{
    void SaveData(SpaceEngineersData data);
    SpaceEngineersData GetData();
    IDatabase Database { get; init; }
}
