using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;

public interface IQuestService
{
    Task<IEnumerable<QuestDto>> GetAll(CancellationToken ct = default);
    Task<QuestDto> GetById(long id, CancellationToken ct = default);
    Task Create(Quest quest, CancellationToken ct = default);
    Task JoinBikerToQuest(long questId, long bikerId, CancellationToken ct = default);
}