using Microsoft.EntityFrameworkCore;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Models;

namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DbContexts;

internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<BikerData> Bikers { get; set; }
    public DbSet<QuestData> Quests { get; set; }
}
