using FluentResults;
using MediatR;
using SpaceEngineers.Data.Collections;

namespace SpaceEngineers.Application.Queries.CubeBlock;
public record GetCubeBlocksQuery() : IRequest<Result<CubeBlockCollection>>;
