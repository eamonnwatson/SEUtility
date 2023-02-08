namespace SEUtility.Data.Models;

internal class Block
{
    public string TypeId { get; set; } = default!;
    public string SubTypeId { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string Description { get; internal set; } = default!;
    public string CubeSize { get; set; } = default!;
    public bool Public { get; set; }
    public decimal BuildTimeSeconds { get; set; }
    public string Components { get; set; } = default!;
    public string CriticalComponent { get; set; } = default!;
    public int PCU { get; set; }
    public string BlueprintClasses { get; set; } = default!;
    public decimal? RefineSpeed { get; set; }
    public decimal? MaterialEfficiency { get; set; }
    public decimal? AssemblySpeed { get; set; }

}
