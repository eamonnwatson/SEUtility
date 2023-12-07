using SpaceEngineers.Parser.Interfaces;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
public class CubeGrid : IModel
{
    public string GridSizeEnum { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    [XmlArray("CubeBlocks")]
    public ICollection<ShipCubeBlock> Prerequisites { get; } = new List<ShipCubeBlock>();

}
