using System.ComponentModel.DataAnnotations.Schema;

using RedPhase.Entities.Base;

namespace RedPhase.Entities.Crm;

public class Organization : AuditableEntity
{
    public string GroupName { get; set; }

    public string GroupNameShort { get; set; }

    public string FullName { get; set; }

    public int? TaxOfficePartyId { get; set; }

    public string TaxOfficeName { get; set; }

    public DateTime? FoundationDate { get; set; }

    public int? FoundationGeoId { get; set; }

    public string PublicOrganization { get; set; }

    public string TradeChamberRegistrationNumber { get; set; }

    [ForeignKey(nameof(Id))]
    public virtual Party Party { get; set; }
}
