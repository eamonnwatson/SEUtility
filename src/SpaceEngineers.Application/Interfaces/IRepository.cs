using FluentResults;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Application.Interfaces;
public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<Result> SaveBlueprints(IEnumerable<Blueprint> blueprints);
    Task<Result> SavePhysicalItems(IEnumerable<PhysicalItem> physicalItems);
    Task<Result> SaveCubeBlocks(IEnumerable<CubeBlock> cubeBlocks);
    Task<Result<IReadOnlyCollection<Blueprint>>> GetBlueprints();
    Task<Result<IReadOnlyCollection<PhysicalItem>>> GetPhysicalItems();
    Task<Result<IReadOnlyCollection<CubeBlock>>> GetCubeBlocks();
}
