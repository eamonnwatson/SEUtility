using FluentResults;
using MediatR;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineers.Application.Commands.CubeBlock;
internal class SaveCubeBlockRequestHandler(IRepository repository) : IRequestHandler<SaveCubeBlocksRequest, Result>
{
    private readonly IRepository _repository = repository;

    public async Task<Result> Handle(SaveCubeBlocksRequest request, CancellationToken cancellationToken)
    {
        await _repository.SaveCubeBlocks(request.CubeBlocks);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
