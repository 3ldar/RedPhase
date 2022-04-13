
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RedPhase.Entities.Base;
using RedPhase.Entities.Identity;
using RedPhase.SharedDependencies;

namespace RedPhase.Crm.Data;

public partial class CrmDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("Users", "Identity", config => config.ExcludeFromMigrations());

        this.AddHilos(modelBuilder);

        modelBuilder.HasDefaultSchema("Crm");

        base.OnModelCreating(modelBuilder);
    }


    public override int SaveChanges() => throw new Exception("Please use SaveChangesAsync instead");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.SaveChangesAsync(true, cancellationToken);


    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = this.ChangeTracker.Entries();
        var auditableEntities = entries
            .Where(r => r.Entity.GetType().IsSubclassOf(typeof(AuditableEntity)))
            .ToArray();

        if (auditableEntities.Any())
        {
            await GenerateAuditRecords(auditableEntities, cancellationToken);
        }

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task GenerateAuditRecords(EntityEntry[] auditableEntities, CancellationToken cancellationToken)
    {
        var context = this.httpContextAccessor.HttpContext;

        if (context == null)
        {
            return;
        }

        var userInfo = context.GetUserInfo();
        var sessionId = userInfo.SessionId;
        var correlationId = Guid.NewGuid();
        var ipAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        var token = await this.Tokens.FindAsync(new object[] { sessionId }, cancellationToken: cancellationToken);

        if (token == null)
        {
            token = new DbToken
            {
                SessionId = sessionId,
                IpAddress = ipAddress,
                ClientId = userInfo.client_id,
                PositionId = userInfo.PositionId,
                OrganizationId = userInfo.OrganizationId,
                RoleId = userInfo.RoleId
            };

            await this.Tokens.AddAsync(token);
        }

        var activity = new DbActivity { Token = token };

        await this.Activities.AddAsync(activity);

        foreach (var item in auditableEntities)
        {
            var auditable = item.Entity as AuditableEntity;

            if (item.State == EntityState.Added)
            {
                auditable.CreateActivityId = activity.Id;
                auditable.ValidFrom = DateTime.UtcNow;
            }

            if (item.State == EntityState.Modified)
            {
                auditable.UpdateActivityId = activity.Id;
            }
        }
    }
}
