using FluentResults;
using MediatR;

namespace SpaceEngineers.Application.Commands.PhysicalItem;
public record SavePhysicalItemsRequest(IEnumerable<Data.Entities.PhysicalItem> PhysicalItems) : IRequest<Result>;
