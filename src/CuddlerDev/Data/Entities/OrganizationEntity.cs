using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CuddlerDev.Data.Attributes;
using Newtonsoft.Json;

namespace CuddlerDev.Data.Entities;

[CuddlerEntity(CuddlerEntityTargets.All)]
[Table("Cuddler_Organizations")]
[JsonObject(MemberSerialization.OptIn)]
public class OrganizationEntity : BaseEntity, IBillingAddress, IHasName
{
    public string? AdvisorId { get; set; }

    public string? Approver { get; set; }

    public string? Attention { get; set; }

    public string[]? BillingContacts { get; set; }

    [JsonProperty]
    public string? CompanyCode { get; set; }

    public string? ContractNumber { get; set; }

    public DateTime? DateOnboarded { get; set; }

    public string? DecisionMaker { get; set; }

    public string? Description { get; set; }

    public string? GeneralLedger { get; set; }

    public string? IndustryIds { get; set; }

    [JsonProperty]
    public string? LogoId { get; set; }

    [JsonProperty]
    public string NameAndCode
    {
        get
        {
            if (string.IsNullOrEmpty(CompanyCode))
            {
                return $"{Name}";
            }

            return $"{Name} &ndash; {CompanyCode}";
        }
    }

    public string? OnboardingContact { get; set; }

    public string? OrganizationSize { get; set; }

    [MaxLength(36)]
    public string? PrimaryAccountContactId { get; set; }

    public string? PurchaseOrderNumber { get; set; }

    // public string? ProgramYears { get; set; }
    public string? Reviewer { get; set; }

    // public string? ProfileTitle { get; set; }
    public string? ShippingAddress { get; set; }

    // public string? ProfileSlogan { get; set; }
    public string? Station { get; set; }

    public string? TechnicalAdmin { get; set; }

    public string? TemplateOption { get; set; }

    public string? BillingCity { get; set; }

    public string? BillingEmail { get; set; }

    public string? BillingPhone { get; set; }

    public string? BillingPostalCode { get; set; }

    public string? BillingProvince { get; set; }

    public string? BillingStreet1 { get; set; }

    public string? BillingStreet2 { get; set; }

    [JsonProperty]
    [Required]
    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return NameAndCode;
    }
}