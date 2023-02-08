using SEUtility.Common.Models;

namespace SEUtility.Parser;

public interface ISBCParser
{
    SpaceEngineersData GetData(string spaceEngineersLocation);
    ShipBlueprint GetBlueprint(string blueprintFile);
}