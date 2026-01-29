namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Models;

internal sealed class BikerData
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<QuestData> Quests { get; set; } = [];
}
