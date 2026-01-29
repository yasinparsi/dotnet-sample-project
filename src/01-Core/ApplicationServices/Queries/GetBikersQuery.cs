using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

public sealed class GetBikersQuery : IRequest<IEnumerable<BikerDto>>
{ }
