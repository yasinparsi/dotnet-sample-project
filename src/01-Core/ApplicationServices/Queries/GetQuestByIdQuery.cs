using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

public sealed class GetQuestByIdQuery : IRequest<QuestDto>
{
    public long Id { get; set; }
}

internal sealed class GetQuestByIdQueryHandler : IRequestHandler<GetQuestByIdQuery, QuestDto>
{
    private readonly IQuestService _questService;

    public GetQuestByIdQueryHandler(IQuestService questService)
    {
        _questService = questService;
    }

    async ValueTask<QuestDto> IRequestHandler<GetQuestByIdQuery, QuestDto>.Handle(GetQuestByIdQuery rq, CancellationToken ct)
    {
        var quest = await _questService.GetById(rq.Id, ct);

        return quest;
    }
}
