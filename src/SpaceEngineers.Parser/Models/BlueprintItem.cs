using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
[DebuggerDisplay("{TypeId,nq}/{SubtypeId,nq}")]
public class BlueprintItem : IModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    [XmlAttribute]
    public decimal Amount { get; set; }
    [XmlAttribute]
    public string TypeId { get; set; } = string.Empty;
    [XmlAttribute]
    public string SubtypeId { get; set; } = string.Empty;
}
