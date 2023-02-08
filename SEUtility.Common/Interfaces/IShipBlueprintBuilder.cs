using SEUtility.Common.Models;

namespace SEUtility.Common.Interfaces;

public interface IShipBlueprintBuilder
{
    IShipBlueprintBuilder SetName(string name);
    IShipBlueprintBuilder SetSize(CubeSize size);
    IShipBlueprintBuilder AddItem(string typeID, string subTypeID);
    ShipBlueprint Build();
}
