
using Microsoft.EntityFrameworkCore;

using RedPhase.Entities.Identity;
using RedPhase.SharedDependencies;

namespace RedPhase.Crm.Data;

public partial class CrmDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("Users", "Identity", config => config.ExcludeFromMigrations());

        this.AddHilos(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
