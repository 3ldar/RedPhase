using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace RedPhase.Identity.Api.Data;

public class PersistedGrantDbContext : DbContext, IPersistedGrantDbContext
{
    public PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options) : base(options)
    {
    }

    public DbSet<PersistedGrant> PersistedGrants { get; set; }

    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    public DbSet<Key> Keys { get; set; }
    public DbSet<ServerSideSession> ServerSideSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Identity");

        modelBuilder.Entity<DeviceFlowCodes>().HasNoKey();
        modelBuilder.Entity<PersistedGrant>().HasNoKey();
    }
}
