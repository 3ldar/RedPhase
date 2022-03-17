using System.ComponentModel.DataAnnotations;

namespace RedPhase.Entities.Base;

public class DbToken
{
    public long Id { get; set; }

    public Guid SessionId { get; set; }

    public int UserId { get; set; }

    public int OrganizationId { get; set; }

    public int PositionId { get; set; }

    public int RoleId { get; set; }

    [MaxLength(40)]
    public string IpAddress { get; set; }

    [MaxLength(25)]
    public string ClientId { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ValidThru { get; set; }

    public virtual ICollection<DbActivity> Activities { get; set; }
}
