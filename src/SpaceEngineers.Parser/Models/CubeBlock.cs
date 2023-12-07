using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SpaceEngineers.Parser.Models;

[XmlRoot("Definition")]
[DebuggerDisplay("{TypeId,nq}/{SubtypeId,nq}")]
public class CubeBlock : IModel
{
    public string FileName { get; set; } = string.Empty;
    public string TypeId { get; set; } = string.Empty;
    public string SubtypeId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string CubeSize { get; set; } = string.Empty;
    public decimal BuildTimeSeconds { get; set; }
    public int PCU { get; set; }
    public bool Public { get; set; }

    [XmlArray]
    public ICollection<ComponentItem> Components { get; } = new List<ComponentItem>();
    [XmlArray]
    public ICollection<BlueprintClassItem> BlueprintClasses { get; set; } = new List<BlueprintClassItem>();
    public ComponentItem? CriticalComponent { get; set; }
    public decimal RefineSpeed { get; set; }
    public decimal MaterialEfficiency { get; set; }

}
