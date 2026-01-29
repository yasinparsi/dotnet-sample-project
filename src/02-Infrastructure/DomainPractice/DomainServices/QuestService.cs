using Microsoft.EntityFrameworkCore;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DbContexts;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Models;
using System.Net;

namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DomainServices;

internal sealed class QuestService : IQuestService
{
    private readonly AppDbContext _dbContext;

    public QuestService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    async Task<IEnumerable<QuestDto>> IQuestService.GetAll(CancellationToken ct)
    {
        var quests = await _dbContext.Quests
            .Select(q => new QuestDto
            {
                Id = q.Id,
                Name = q.Name,
                Incentive = q.Incentive,
            })
            .ToListAsync(ct);

        return quests;
    }

    async Task<QuestDto> IQuestService.GetById(long id, CancellationToken ct)
    {
        var quest = await _dbContext.Quests.Select(q => new QuestDto
        {
            Id = q.Id,
            Name = q.Name,
            Incentive = q.Incentive,
        }).FirstOrDefaultAsync(ct);

        if (quest is null)
        {
            throw new SnappFoodException(HttpStatusCode.NotFound, TraceIds.QUEST_NOT_FOUND,
                "چنین کویستی یافت نشد");
        }

        return quest;
    }

    async Task IQuestService.Create(Quest quest, CancellationToken ct)
    {
        QuestData data = new()
        {
            Id = quest.Id,
            Incentive = quest.Incentive,
            Name = quest.Name,
        };

        await _dbContext.Quests.AddAsync(data, ct);
    }

    async Task IQuestService.JoinBikerToQuest(long questId, long bikerId, CancellationToken ct)
    {
        var bikerData = await _dbContext.Bikers.FindAsync([bikerId], ct);
        if (bikerData is null)
        {
            throw new SnappFoodException(HttpStatusCode.NotFound, TraceIds.BIKER_NOT_FOUND,
                "چنین بایکری یافت نشد");
        }
        var quests = bikerData.Quests.Select(q => Quest.Load(q.Id, q.Name, q.Incentive));
        var biker = Biker.Load(bikerId, bikerData.FirstName, bikerData.LastName, quests);

        var questData = await _dbContext.Quests.FirstOrDefaultAsync(q => q.Id == questId, ct);
        if (questData is null)
        {
            throw new SnappFoodException(HttpStatusCode.NotFound, TraceIds.QUEST_NOT_FOUND,
                "چنین کویستی یافت نشد");
        }
        var quest = Quest.Create(questData.Id, questData.Name, questData.Incentive);

        biker.AddQuest(quest);
        bikerData.Quests.Add(questData);
    }
}
