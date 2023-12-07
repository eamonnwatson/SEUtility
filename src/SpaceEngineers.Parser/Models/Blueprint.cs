using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;
[DebuggerDisplay("{TypeId,nq}/{SubtypeId,nq}")]
public class Blueprint : IModel
{
    public string FileName { get; set; } = string.Empty;
    public string TypeId { get; set; } = string.Empty;
    public string SubtypeId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public decimal BaseProductionTimeInSeconds { get; set; }
    [XmlArray]
    public ICollection<BlueprintItem> Prerequisites { get; } = new List<BlueprintItem>();
    [XmlArray]
    public ICollection<BlueprintItem> Results { get; } = new List<BlueprintItem>();
    public BlueprintItem? Result { get; set; }

}
