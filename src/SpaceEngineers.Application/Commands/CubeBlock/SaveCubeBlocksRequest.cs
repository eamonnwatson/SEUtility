using FluentResults;
using MediatR;

namespace SpaceEngineers.Application.Commands.CubeBlock;
public record SaveCubeBlocksRequest(IEnumerable<Data.Entities.CubeBlock> CubeBlocks) : IRequest<Result>;
