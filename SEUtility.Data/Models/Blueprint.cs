namespace SEUtility.Data.Models;

internal class Blueprint
{
    public string TypeId { get; set; } = default!;
    public string SubTypeId { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public decimal BaseProductionTimeInSeconds { get; set; }
    public bool IsPrimary { get; set; }
    public string Prerequisites { get; set; } = default!;
    public string Results { get; set; } = default!;
}
