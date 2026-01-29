namespace SnappFood.DotNetSampleProject.Core.DomainModels.General;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken ct = default);
}
