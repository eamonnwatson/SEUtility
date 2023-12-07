using SpaceEngineers.Parser.Interfaces;
using System.Diagnostics;

namespace SpaceEngineers.Parser.Models;
[DebuggerDisplay("{TypeId,nq}/{SubtypeId,nq}")]
public class PhysicalItem : IModel
{
    public string FileName { get; set; } = string.Empty;
    public string TypeId { get; set; } = string.Empty;
    public string SubtypeId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public decimal Mass { get; set; }
    public decimal Volume { get; set; }
    public bool CanPlayerOrder { get; set; }
}
