
using System.ComponentModel.DataAnnotations;

using RedPhase.Entities.Base;
using RedPhase.Entities.Enum;

namespace RedPhase.Entities.Crm;

public class Party : AuditableEntity
{
    public PartyType PartyType { get; set; }

    [Required]
    [MaxLength(255)]
    public string IdentificationNumber { get; set; }

    [Required]
    [MaxLength(255)]
    public string PartyName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public DateTime InitialContactDate { get; set; }

    public DateTime LastContactDate { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Organization Organization { get; set; }
}
