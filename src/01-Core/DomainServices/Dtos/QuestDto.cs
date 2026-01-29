namespace SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

public sealed class QuestDto
{
    public long Id { get; init; }
    public string Name { get; init; } = null!;
    public decimal Incentive { get; init; }
}