using FluentResults;
using MediatR;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineers.Application.Commands.PhysicalItem;
internal class SavePhysicalItemsRequestHandler(IRepository repository) : IRequestHandler<SavePhysicalItemsRequest, Result>
{
    private readonly IRepository _repository = repository;

    public async Task<Result> Handle(SavePhysicalItemsRequest request, CancellationToken cancellationToken)
    {
        await _repository.SavePhysicalItems(request.PhysicalItems);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
