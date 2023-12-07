using MediatR;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineers.Application.Commands;

internal class ResetDatabaseCommandHander(IUnitOfWork unitOfWork) : IRequestHandler<ResetDatabaseCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(ResetDatabaseCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.PrepareDatabase();
    }
}
