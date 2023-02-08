namespace SEUtility.Common.Models;

public struct BlueprintItem
{
    public string TypeID { get; init; }
    public string SubTypeID { get; init; }
    public decimal Amount { get; init; }
    public BlueprintItem(string typeID, string subTypeID, decimal amount)
    {
        TypeID = typeID;
        SubTypeID = subTypeID;
        Amount = amount;
    }
    public override string ToString()
    {
        return $"{Amount} {TypeID}/{SubTypeID}";
    }
}
