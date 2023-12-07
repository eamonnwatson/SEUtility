using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Application.Interfaces;
public interface IReportWriter
{
    string CreateReport(ShipBlueprint shipBlueprint);
}
