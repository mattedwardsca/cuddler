using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Profiles")]
[JsonObject(MemberSerialization.OptIn)]
public class ProfileEntity : BaseEntity, IProfileRole, IProfile
{
    public string? AboutTags { get; set; }

    public string? ActiveRole { get; set; }

    [JsonProperty]
    public string? Bio { get; set; }

    [JsonProperty]
    public string? BirthDate { get; set; }

    public string? BusinessNumber { get; set; }

    [JsonProperty]
    public string? CityOfDestination { get; set; }

    [JsonProperty]
    public string? CityOfOrigin { get; set; }

    [JsonProperty]
    public string? CityOfOriginResidence { get; set; }

    [JsonProperty]
    public string? CostCenter { get; set; }

    [JsonProperty]
    public string? CountryOfDestination { get; set; }

    public string? CustomerStatus { get; set; }

    public string? CustomerStatusNote { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateSuspensionEnded { get; set; }

    public DateTime? DateSuspensionRequested { get; set; }

    public DateTime? DateSuspensionStarted { get; set; }

    public string? DeliveryPreference { get; set; }

    public string? Description { get; set; }

    public string? EmploymentStatus { get; set; }

    public string? FaxNumber { get; set; }

    public string? FederalProgramName { get; set; }

    public string? FederalRidingName { get; set; }

    public string? FederalRidingNumber { get; set; }

    [JsonProperty]
    public virtual string FirstLastName => $"{Name} {LastName}".Trim();

    [JsonProperty]
    public virtual string NameAndCompany => GetNameAndCompany();

    public string? Gender { get; set; }

    //[DefaultValue(501040)]
    [JsonProperty]
    public string? GeneralLedger { get; set; }

    public DateTime? HireDate { get; set; }

    [JsonProperty]
    public string? InternalOrderNo { get; set; }

    [JsonProperty]
    public bool IsBlacklisted { get; set; }

    [JsonProperty]
    public bool IsVip { get; set; }

    [JsonProperty]
    public string? LastName { get; set; }

    public string? Location { get; set; }

    public string? MaritalStatus { get; set; }

    public string? MiddleName { get; set; }

    public string? NaicsIdentifier { get; set; }

    public string? Nationality { get; set; }

    public DateTime? ProjectedTerminationDate { get; set; }

    public string? RefNumber { get; set; }

    public DateTime? RoeCompleted { get; set; }

    [JsonProperty]
    public virtual string Roles => GetRoles();

    public string? SuspensionNotes { get; set; }

    public string? Terms { get; set; }

    public string? ThumbnailId { get; set; }

    [DisplayName("Time Zone")]
    public string? TimeZone { get; set; }

    [JsonProperty]
    public string? OrganizationId { get; set; }

    [JsonProperty]
    [ForeignKey(nameof(OrganizationId))]
    public OrganizationEntity? Organization { get; set; }

    [JsonProperty]
    [Required]
    public string Name { get; set; } = null!;

    public bool IsAdvisor { get; set; }

    public bool IsAuditor { get; set; }

    public bool IsCoordinator { get; set; }

    [JsonProperty]
    public bool IsOrganizationAdmin { get; set; }


    public string GetFullName()
    {
        var name = $"{Name} {MiddleName} {LastName}".Trim();

        return name;
    }

    public string GetNameAndCompany()
    {
        var name = $"{Name} {LastName}".Trim();

        if (Organization != null)
        {
            name += $"&nbsp;({Organization.Name})";
        }

        return name;
    }

    public string GetNameAndCompanyBirthDate()
    {
        var name = GetNameAndCompany();

        if (!string.IsNullOrEmpty(BirthDate))
        {
            name += $"&nbsp;b.{BirthDate}";
        }

        return name;
    }

    public string GetName()
    {
        return $"{Name} {LastName}".Trim();
    }

    public string GetRoles()
    {
        var list = new List<string>();

        if (IsAdvisor)
        {
            list.Add("Advisor");
        }

        if (IsAuditor)
        {
            list.Add("Auditor");
        }

        if (IsCoordinator)
        {
            list.Add("Coordinator");
        }

        if (IsOrganizationAdmin)
        {
            list.Add("Organization Admin");
        }

        if (!list.Any())
        {
            list.Add("Employee");
        }

        return string.Join(", ", list);
    }

    public override string ToString()
    {
        return GetName();
    }
}