namespace SEUtility.Common.Models;

public class CubeBlock : BaseItem
{
    public string Description { get; internal set; } = default!;
    public CubeSize CubeSize { get; init; }
    public bool Public { get; init; }
    public decimal BuildTimeSeconds { get; init; }
    public IReadOnlyList<ComponentItem> Components { get; init; } = default!;
    public ComponentItem CriticalComponent { get; init; }
    public int PCU { get; set; }
    public IReadOnlyList<string> BlueprintClasses { get; init; } = default!;
    public decimal? RefineSpeed { get; init; }
    public decimal? MaterialEfficiency { get; init; }
    public decimal? AssemblySpeed { get; init; }

    internal CubeBlock() { }

    public override string ToString()
    {
        return "CubeBlock: " + base.ToString();
    }
}
