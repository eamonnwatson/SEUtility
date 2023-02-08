using SEUtility.Common.Interfaces;

namespace SEUtility.Parser.Parsers;

public interface IShipParser
{
    void Parse(string filename, IShipBlueprintBuilder builder);
}
