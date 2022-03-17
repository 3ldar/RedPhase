using System.ComponentModel.DataAnnotations.Schema;

namespace RedPhase.Entities.Base;

[NotMapped]
public class AuditableEntity : EntityWithDates
{
    public long CreateActivityId { get; set; }

    public long? UpdateActivityId { get; set; }

    [ForeignKey(nameof(CreateActivityId))]
    public DbActivity CreateActivity { get; set; }

    [ForeignKey(nameof(UpdateActivityId))]
    public DbActivity UpdateActivity { get; set; }
}
