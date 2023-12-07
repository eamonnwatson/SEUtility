using SpaceEngineers.Parser.Interfaces;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
public class ShipCubeBlock : IModel
{
    [XmlAttribute("type", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public string Type { get; set; } = string.Empty;
    public string SubtypeName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}

