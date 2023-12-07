namespace SpaceEngineers.Data.Entities;

public class PhysicalItem : BaseItem
{
    public decimal Mass { get; private init; }
    public decimal Volume { get; private init; }
    public bool CanPlayerOrder { get; private init; }

    private PhysicalItem(Guid id, string? type, string typeId, string subTypeId, string displayName, string fileName,
                decimal mass, decimal volume, bool canPlayerOrder)
        : base(id, type, typeId, subTypeId, displayName, fileName)
    {
        Mass = mass;
        Volume = volume;
        CanPlayerOrder = canPlayerOrder;
    }

    public static PhysicalItem Create(string? type, string typeId, string subtypeId, string displayName, string fileName,
                               decimal mass, decimal volume, bool canPlayerOrder)
    {
        return new PhysicalItem(Guid.NewGuid(), type, typeId, subtypeId, displayName, fileName, mass, volume, canPlayerOrder);
    }

}
