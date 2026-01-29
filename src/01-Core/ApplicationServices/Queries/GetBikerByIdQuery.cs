using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

public sealed class GetBikerByIdQuery : IRequest<BikerDto>
{
    public long Id { get; init; }
}
