using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

internal sealed class GetQuestsQueryHandler : IRequestHandler<GetQuestsQuery, IEnumerable<QuestDto>>
{
    private readonly IQuestService _questService;

    public GetQuestsQueryHandler(IQuestService questService)
    {
        _questService = questService;
    }

    async ValueTask<IEnumerable<QuestDto>> IRequestHandler<GetQuestsQuery, IEnumerable<QuestDto>>
        .Handle(GetQuestsQuery rq, CancellationToken ct)
    {
        var quests = await _questService.GetAll(ct);
        return quests;
    }
}