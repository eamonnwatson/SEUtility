using FluentResults;
using MediatR;

namespace SpaceEngineers.Application.Commands.Blueprint;
public record SaveBlueprintRequest(IEnumerable<Data.Entities.Blueprint> Blueprints) : IRequest<Result>;
