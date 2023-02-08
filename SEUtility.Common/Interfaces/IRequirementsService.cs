using SEUtility.Common.Models;

namespace SEUtility.Common.Interfaces;

public interface IRequirementsService
{
    SpaceEngineersData? Data { get; set; }
    Recipe GetRequirements(ShipBlueprint blueprint);
}
