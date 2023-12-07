namespace SpaceEngineers.Data.Entities;

public class PhysicalItemListItem
{
    public Guid Id { get; internal set; } = Guid.NewGuid();
    public decimal Quantity { get; internal init; }
    public PhysicalItem Component { get; internal init; } = default!;
    public override string ToString()
    {
        return $"{Quantity} - {Component}";
    }
};
