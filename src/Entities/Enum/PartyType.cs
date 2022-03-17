using System.ComponentModel.DataAnnotations;

namespace RedPhase.Entities.Enum;


public enum PartyType
{
    [Display(Name = "Gerçek Kişi")]
    Customer = 1,

    [Display(Name = "Kurum")]
    LegalOrganization = 2,

    [Display(Name = "İç Organizasyon")]
    InternalOrganization = 3,

    [Display(Name = "İş Ortağı")]
    BusinessPartner = 4,

    [Display(Name = "Personel")]
    Person = 5
}

