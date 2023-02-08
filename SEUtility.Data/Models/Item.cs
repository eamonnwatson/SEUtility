namespace SEUtility.Data.Models;

internal class Item
{
    public string Type { get; set; } = default!;
    public string TypeId { get; set; } = default!;
    public string SubTypeId { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public decimal Mass { get; init; }
    public decimal Volume { get; init; }
    public bool CanPlayerOrder { get; init; }
    public int Capacity { get; init; }

}
