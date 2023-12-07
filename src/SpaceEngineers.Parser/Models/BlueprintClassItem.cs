using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
[DebuggerDisplay("{Class,nq}")]
public class BlueprintClassItem : IModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    [XmlText]
    public string Class { get; set; } = string.Empty;
}
