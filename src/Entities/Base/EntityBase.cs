using System.ComponentModel.DataAnnotations.Schema;

namespace RedPhase.Entities.Base;

[NotMapped]
public class EntityBase
{
    public int Id { get; set; }
}
