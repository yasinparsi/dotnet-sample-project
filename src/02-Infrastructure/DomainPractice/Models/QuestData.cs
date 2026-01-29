namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Models;

internal sealed class QuestData
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Incentive { get; set; }
}