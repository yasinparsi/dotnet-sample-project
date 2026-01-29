using Mediator;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;

internal sealed class RegisterBikerCommandHandler : IRequestHandler<RegisterBikerCommand, long>
{
    private readonly IBikerService _bikerService;
    private readonly IQuestService _questService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBikerCommandHandler(IBikerService bikerService, IQuestService questService, IUnitOfWork unitOfWork)
    {
        _bikerService = bikerService;
        _questService = questService;
        _unitOfWork = unitOfWork;
    }

    async ValueTask<long> IRequestHandler<RegisterBikerCommand, long>.Handle(RegisterBikerCommand rq, CancellationToken ct)
    {
        var bikerId = Random.Shared.Next(1_000_000, 9_999_999);
        var biker = Biker.Create(bikerId, rq.FirstName, rq.LastName);
        await _bikerService.Create(biker, ct);

        var quests = await _questService.GetAll(ct);
        var randomQuestId = quests.Shuffle().First().Id;
        await _questService.JoinBikerToQuest(randomQuestId, biker.Id, ct);

        await _unitOfWork.SaveChanges(ct);

        return bikerId;
    }
}