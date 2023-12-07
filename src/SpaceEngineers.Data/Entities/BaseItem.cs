namespace SpaceEngineers.Data.Entities;
public abstract class BaseItem : Entity
{
    public string? Type { get; private init; }
    public string TypeId { get; private init; }
    public string SubTypeId { get; private init; }
    public string DisplayName { get; private init; }
    public string FileName { get; private init; }

    protected BaseItem(Guid id, string? type, string typeId, string subTypeId, string displayName, string fileName)
        : base(id)
    {
        Type = type;
        TypeId = typeId;
        SubTypeId = subTypeId;
        DisplayName = displayName;
        FileName = fileName;
    }

    public override string ToString()
    {
        return $"{TypeId}/{SubTypeId} : {DisplayName}";
    }
}
