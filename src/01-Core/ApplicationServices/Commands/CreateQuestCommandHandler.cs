using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;

internal sealed class CreateQuestCommandHandler : IRequestHandler<CreateQuestCommand, long>
{
    private readonly IQuestService _questService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestCommandHandler(IQuestService questService, IUnitOfWork unitOfWork)
    {
        _questService = questService;
        _unitOfWork = unitOfWork;
    }

    async ValueTask<long> IRequestHandler<CreateQuestCommand, long>.Handle(CreateQuestCommand rq, CancellationToken ct)
    {
        var questId = Random.Shared.Next();
        var quest = Quest.Create(questId, rq.Name, rq.Incentive);
        await _questService.Create(quest, ct);

        await _unitOfWork.SaveChanges(ct);

        return questId;
    }
}