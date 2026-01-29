namespace SnappFood.DotNetSampleProject.Core.DomainModels.Models;

public sealed class Quest
{
    private Quest()
    { }

    public static Quest Create(long id, string name, decimal incentive)
    {
        //if there is logic on creation

        return new Quest
        {
            Id = id,
            Name = name,
            Incentive = incentive,
        };
    }

    public static Quest Load(long id, string name, decimal incentive)
    {
        return new Quest
        {
            Id = id,
            Name = name,
            Incentive = incentive,
        };
    }

    public long Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal Incentive { get; set; }
}
