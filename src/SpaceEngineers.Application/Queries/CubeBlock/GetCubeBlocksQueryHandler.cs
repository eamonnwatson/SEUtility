using FluentResults;
using MediatR;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Data.Collections;

namespace SpaceEngineers.Application.Queries.CubeBlock;

public class GetCubeBlocksQueryHandler(IRepository repository) : IRequestHandler<GetCubeBlocksQuery, Result<CubeBlockCollection>>
{
    private readonly IRepository _repository = repository;
    public async Task<Result<CubeBlockCollection>> Handle(GetCubeBlocksQuery request, CancellationToken cancellationToken)
    {
        var cubes = await _repository.GetCubeBlocks();
        if (cubes.IsFailed)
            return cubes.ToResult();

        return new CubeBlockCollection(cubes.Value);

    }
}
