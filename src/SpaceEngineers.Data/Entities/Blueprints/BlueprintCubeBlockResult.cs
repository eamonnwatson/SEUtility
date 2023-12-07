using SpaceEngineers.Data.Collections;

namespace SpaceEngineers.Data.Entities.Blueprints;
public class BlueprintCubeBlockResult(CubeBlockCollection foundBlocks)
{
    public CubeBlockCollection Found { get; private init; } = foundBlocks;
    public IEnumerable<BlueprintCubeBlock> NotFound { get; private init; } = Enumerable.Empty<BlueprintCubeBlock>();
    public BlueprintCubeBlockResult(CubeBlockCollection foundBlocks, IEnumerable<BlueprintCubeBlock> notFoundBlocks) : this(foundBlocks)
    {
        NotFound = notFoundBlocks;
    }
}
