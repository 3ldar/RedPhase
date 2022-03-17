using System.ComponentModel.DataAnnotations;

namespace RedPhase.Entities.Base;

public class DbActivity
{
    public long Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Host { get; set; }

    public virtual DbToken Token { get; set; }
}
