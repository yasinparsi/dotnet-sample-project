namespace SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

public sealed class BikerDto
{
    public long Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public IEnumerable<QuestDto> Quests { get; init; } = [];
}
