namespace SnappFood.DotNetSampleProject.App.Api.Models;

public sealed class CreateQuestDto
{
    public string Name { get; init; } = null!;
    public decimal Incentive { get; init; }
}
