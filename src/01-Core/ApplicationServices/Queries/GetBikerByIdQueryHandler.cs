using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

internal sealed class GetBikerByIdQueryHandler : IRequestHandler<GetBikerByIdQuery, BikerDto>
{
    private readonly IBikerService _bikerService;

    public GetBikerByIdQueryHandler(IBikerService bikerService)
    {
        _bikerService = bikerService;
    }

    async ValueTask<BikerDto> IRequestHandler<GetBikerByIdQuery, BikerDto>.Handle(GetBikerByIdQuery rq, CancellationToken ct)
    {
        var biker = await _bikerService.GetById(rq.Id, ct);

        return biker;
    }
}
