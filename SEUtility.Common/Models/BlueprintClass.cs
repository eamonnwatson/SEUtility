namespace SEUtility.Common.Models;

public class BlueprintClass : BaseItem
{
    private readonly List<string> blueprintEntries = new();
    public string Description { get; init; } = default!;
    public IReadOnlyList<string> SubTypeIDList { get => blueprintEntries; }

    internal BlueprintClass() { }
    public override string ToString()
    {
        return "BlueprintClass: " + base.ToString();
    }
    internal void AddEntry(string entry)
    {
        blueprintEntries.Add(entry);
    }

}
