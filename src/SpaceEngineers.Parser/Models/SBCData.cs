namespace SpaceEngineers.Parser.Models;
public class SBCData
{
    public IReadOnlyList<AmmoMagazine> AmmoMagazines { get; internal set; } = new List<AmmoMagazine>();
    public IReadOnlyList<Blueprint> Blueprints { get; internal set; } = new List<Blueprint>();
    public IReadOnlyList<BlueprintClass> BlueprintClasss { get; internal set; } = new List<BlueprintClass>();
    public IReadOnlyList<BlueprintClassEntry> BlueprintClassEntries { get; internal set; } = new List<BlueprintClassEntry>();
    public IReadOnlyList<Component> Components { get; internal set; } = new List<Component>();
    public IReadOnlyList<CubeBlock> CubeBlocks { get; internal set; } = new List<CubeBlock>();
    public IReadOnlyList<Localization> Localizations { get; internal set; } = new List<Localization>();
    public IReadOnlyList<PhysicalItem> PhysicalItems { get; internal set; } = new List<PhysicalItem>();
}
