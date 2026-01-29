using Mediator;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;

public sealed class CreateQuestCommand : IRequest<long>
{
    public string Name { get; init; } = null!;
    public decimal Incentive { get; init; }
}
