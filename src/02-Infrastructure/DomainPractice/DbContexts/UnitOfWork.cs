using SnappFood.DotNetSampleProject.Core.DomainModels.General;

namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DbContexts;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    async Task IUnitOfWork.SaveChanges(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}
