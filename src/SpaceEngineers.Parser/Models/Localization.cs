using SpaceEngineers.Parser.Interfaces;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;

[XmlRoot("data")]
public class Localization : IModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;
    [XmlElement("value")]
    public string Value { get; set; } = string.Empty;
}
