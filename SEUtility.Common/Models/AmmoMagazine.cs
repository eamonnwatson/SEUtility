namespace SEUtility.Common.Models;

public sealed class AmmoMagazine : PhysicalItem
{
    public int Capacity { get; init; }
    internal AmmoMagazine() { }
    public override string ToString()
    {
        return "AmmoMagazine: " + base.ToString();
    }

}
