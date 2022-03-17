
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using RedPhase.Entities.Base;

namespace RedPhase.Entities.Crm;

public class Customer : AuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string MiddleName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string FatherName { get; set; }

    [MaxLength(100)]
    public string MotherName { get; set; }

    public DateTime DateOfBirth { get; set; }

    [MaxLength(100)]
    public string MotherMaidenName { get; set; }

    [MaxLength(100)]
    public string MaidenName { get; set; }

    public DateTime? MarriageDate { get; set; }

    [Required]
    [MaxLength(5)]
    public string Language { get; set; }

    [ForeignKey(nameof(Id))]
    public virtual Party Party { get; set; }
}
