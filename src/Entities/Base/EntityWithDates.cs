using System.ComponentModel.DataAnnotations.Schema;

namespace RedPhase.Entities.Base;

[NotMapped]
public class EntityWithDates : EntityBase
{
    public DateTime ValidFrom { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public DateTime ValidThru { get; set; } = DateTime.MaxValue;
}
