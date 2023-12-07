using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;

[XmlRoot("Entry")]
[DebuggerDisplay("{BlueprintsubtypeId,nq}")]
public class BlueprintClassEntry : IModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    [XmlAttribute]
    public string Class { get; set; } = string.Empty;
    [XmlAttribute]
    public string BlueprintSubtypeId { get; set; } = string.Empty;
}
