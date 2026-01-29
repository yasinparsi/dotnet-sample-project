using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

internal sealed class GetBikersQueryHandler : IRequestHandler<GetBikersQuery, IEnumerable<BikerDto>>
{
    private readonly IBikerService _bikerService;
    private readonly IUnitOfWork _unitOfWork;

    public GetBikersQueryHandler(IBikerService bikerService, IUnitOfWork unitOfWork)
    {
        _bikerService = bikerService;
        _unitOfWork = unitOfWork;
    }

    async ValueTask<IEnumerable<BikerDto>> IRequestHandler<GetBikersQuery, IEnumerable<BikerDto>>
        .Handle(GetBikersQuery rq, CancellationToken ct)
    {
        var bikers = await _bikerService.GetAll(ct);
        return bikers;
    }
}
