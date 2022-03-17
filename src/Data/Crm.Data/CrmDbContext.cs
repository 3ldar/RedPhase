
using Microsoft.EntityFrameworkCore;

using RedPhase.Entities.Base;
using RedPhase.Entities.Crm;
using RedPhase.Entities.Identity;

namespace RedPhase.Crm.Data;

public partial class CrmDbContext : DbContext
{
    public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
    {
    }

    public virtual DbSet<DbActivity> Activities { get; set; }

    public virtual DbSet<DbToken> Tokens { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<IdentityUser> Users { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }
}
