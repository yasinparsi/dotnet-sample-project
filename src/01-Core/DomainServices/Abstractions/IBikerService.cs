using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;

public interface IBikerService
{
    Task<IEnumerable<BikerDto>> GetAll(CancellationToken ct = default);
    Task<BikerDto> GetById(long id, CancellationToken ct = default);
    Task Create(Biker biker, CancellationToken ct = default);
}
