using FluentResults;
using Microsoft.EntityFrameworkCore;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Data.Entities;
using System.Data.Common;

namespace SpaceEngineers.Persistence;
internal class Repository(ApplicationDbContext context) : IRepository
{
    private readonly ApplicationDbContext _context = context;

    public IUnitOfWork UnitOfWork { get => _context; }

    public async Task<Result<IReadOnlyCollection<Blueprint>>> GetBlueprints()
    {
        try
        {
            return await _context.Blueprints
                .AsNoTracking()
                .ToListAsync();
        }
        catch (DbException ex)
        {
            return Result.Fail(new Error("Database Failure").CausedBy(ex));
        }
    }

    public async Task<Result<IReadOnlyCollection<CubeBlock>>> GetCubeBlocks()
    {
        try
        {
            var pcb = await _context.ProductionCubeBlocks.AsSplitQuery().ToListAsync();
            var cb = await _context.CubeBlocks.AsSplitQuery().Where(c => c.GetType() != typeof(ProductionCubeBlock)).ToListAsync();

            return cb.Concat(pcb).OrderBy(cb => cb.TypeId).ThenBy(cb => cb.SubTypeId).ToList();
        }
        catch (DbException ex)
        {
            return Result.Fail(new Error("Database Failure").CausedBy(ex));
        }
    }

    public async Task<Result<IReadOnlyCollection<PhysicalItem>>> GetPhysicalItems()
    {
        try
        {
            return await _context.PhysicalItems.AsNoTracking().ToListAsync();
        }
        catch (DbException ex)
        {
            return Result.Fail(new Error("Database Failure").CausedBy(ex));
        }
    }

    public async Task<Result> SaveBlueprints(IEnumerable<Blueprint> blueprints)
    {
        await _context.Blueprints.AddRangeAsync(blueprints);
        return Result.Ok();
    }

    public async Task<Result> SaveCubeBlocks(IEnumerable<CubeBlock> cubeBlocks)
    {
        var pcb = cubeBlocks.Where(a => a is ProductionCubeBlock).Cast<ProductionCubeBlock>();
        var cb = cubeBlocks.Where(a => a is not ProductionCubeBlock);

        await _context.CubeBlocks.AddRangeAsync(cb);
        await _context.ProductionCubeBlocks.AddRangeAsync(pcb);

        return Result.Ok();
    }

    public async Task<Result> SavePhysicalItems(IEnumerable<PhysicalItem> physicalItems)
    {
        await _context.PhysicalItems.AddRangeAsync(physicalItems);
        return Result.Ok();
    }
}
