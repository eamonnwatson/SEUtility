using FluentResults;
using MediatR;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineers.Application.Commands.Blueprint;
internal class SaveBlueprintsRequestHandler(IRepository repository) : IRequestHandler<SaveBlueprintRequest, Result>
{
    private readonly IRepository _repository = repository;

    public async Task<Result> Handle(SaveBlueprintRequest request, CancellationToken cancellationToken)
    {
        await _repository.SaveBlueprints(request.Blueprints);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
