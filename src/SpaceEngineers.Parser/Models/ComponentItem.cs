using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
[DebuggerDisplay("{Subtype,nq}")]
public class ComponentItem : IModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    [XmlAttribute]
    public string Subtype { get; set; } = string.Empty;
    [XmlAttribute]
    public int Count { get; set; }
    [XmlAttribute]
    public int Index { get; set; }
}
