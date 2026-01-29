using Microsoft.EntityFrameworkCore;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainModels.Models;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Core.DomainServices.Dtos;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DbContexts;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Models;
using System.Net;

namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DomainServices;

internal sealed class BikerService : IBikerService
{
    private readonly AppDbContext _dbContext;

    public BikerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    async Task<IEnumerable<BikerDto>> IBikerService.GetAll(CancellationToken ct)
    {
        var bikers = await _dbContext.Bikers.Select(b => new BikerDto
        {
            Id = b.Id,
            FirstName = b.FirstName,
            LastName = b.LastName,
            Quests = b.Quests.Select(q => new QuestDto
            {
                Id = q.Id,
                Name = q.Name,
                Incentive = q.Incentive,
            }).ToList(),
        }).ToListAsync(ct);

        return bikers;
    }

    async Task IBikerService.Create(Biker biker, CancellationToken ct)
    {
        BikerData data = new()
        {
            Id = biker.Id,
            FirstName = biker.FirstName,
            LastName = biker.LastName,
            Quests = biker.Quests.Select(q => new QuestData
            {
                Id = q.Id,
                Incentive = q.Incentive,
                Name = q.Name,
            }).ToList(),
        };

        await _dbContext.Bikers.AddAsync(data, ct);
    }

    async Task<BikerDto> IBikerService.GetById(long id, CancellationToken ct)
    {
        var data = await _dbContext.Bikers.FirstOrDefaultAsync(b => b.Id == id, ct);

        if (data is null)
        {
            throw new SnappFoodException(HttpStatusCode.NotFound, TraceIds.BIKER_NOT_FOUND,
                "چنین بایکری یافت نشد");
        }

        return new BikerDto
        {
            Id = id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Quests = data.Quests.Select(q => new QuestDto
            {
                Id = q.Id,
                Name = q.Name,
                Incentive = q.Incentive,
            }),
        };
    }
}
