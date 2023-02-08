namespace SEUtility.Common.Models;

public class BlockCategory : BaseItem
{
    public string Name { get; set; } = default!;
    public IReadOnlyList<string> ItemIDs { get; set; } = default!;
    internal BlockCategory() { }
    public override string ToString()
    {
        return "BlockCategory: " + DisplayName;
    }

}
