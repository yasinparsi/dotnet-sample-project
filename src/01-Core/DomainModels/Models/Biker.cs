using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using System.Net;

namespace SnappFood.DotNetSampleProject.Core.DomainModels.Models;

public sealed class Biker
{
    private readonly List<Quest> _quests = [];

    private Biker()
    { }

    public static Biker Create(long id, string firstName, string lastName)
    {
        return new Biker
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
        };
    }

    public static Biker Load(long id, string firstName, string lastName, IEnumerable<Quest> quests)
    {
        var biker = new Biker
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
        };
        biker._quests.AddRange(quests);

        return biker;
    }

    public long Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public IReadOnlyCollection<Quest> Quests => _quests.AsReadOnly();

    public void AddQuest(Quest quest)
    {
        if (_quests.Any(q => q.Id == quest.Id))
        {
            throw new SnappFoodException(HttpStatusCode.BadRequest, TraceIds.REPETITIVE_QUEST,
                "این کویست برای بایکر قبلا ثبت شده است");
        }

        _quests.Add(quest);
    }
}
