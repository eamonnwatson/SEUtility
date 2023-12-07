using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
[XmlRoot("Class")]
[DebuggerDisplay("{TypeId,nq}/{SubtypeId,nq}")]
public class BlueprintClass : IModel
{
    public string FileName { get; set; } = string.Empty;
    public string TypeId { get; set; } = string.Empty;
    public string SubtypeId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}
