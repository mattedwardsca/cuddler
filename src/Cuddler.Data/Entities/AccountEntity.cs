using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Cuddler.Data.Entities;

[Table("AspnetUsers")]
[JsonObject(MemberSerialization.OptIn)]
public class AccountEntity : IdentityUser, IData, IAccount
{
    public DateTime? DatePasswordChanged { get; set; }

    [JsonProperty]
    public string FormattedName => $"#{UserName}- {Profile.GetFullName()} ";

    [DisplayName("Prefered Language")]
    public string? Language { get; set; }

    [JsonProperty]
    public virtual bool Locked => IsLocked();

    public string? LoginToken { get; set; }

    public DateTime? LoginTokenExpiration { get; set; }

    [JsonProperty]
    public new string? PhoneNumber
    {
        get => base.PhoneNumber;
        set => base.PhoneNumber = value;
    }

    [JsonProperty]
    public virtual ProfileEntity Profile { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(ProfileEntity))]
    public string ProfileId { get; set; } = null!;

    public DateTime? SuperUserExpiration { get; set; }

    public IProfile GetProfile()
    {
        return Profile;
    }

    [JsonProperty]
    [DisplayName("Email Address")]
    public new string Email
    {
#pragma warning disable CS8603
        get => base.Email;
#pragma warning restore CS8603
        set => base.Email = value;
    }

    public bool IsAdmin { get; set; }

    [DisplayName("Account Number")]
    [JsonProperty]
    public new string UserName
    {
#pragma warning disable CS8603
        get => base.UserName;
#pragma warning restore CS8603
        set => base.UserName = value;
    }

    public DateTime? DateArchived { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? PolicyTagId { get; set; }

    [MaxLength(36)]
    [JsonProperty]
    public override string Id
    {
        get => base.Id;
        set => base.Id = value;
    }

    public virtual bool CanAdvisor()
    {
        return IsAdmin || Profile.IsAdvisor;
    }

    public virtual bool CanCoordinator()
    {
        return IsAdmin || Profile.IsCoordinator;
    }

    public virtual bool CanEmployee()
    {
        return IsAdmin || !Profile.IsOrganizationAdmin && !Profile.IsAdvisor && !Profile.IsAuditor && !Profile.IsCoordinator;
    }

    public string GetName()
    {
        return Profile.Name;
    }

    public virtual bool IsArchived()
    {
        return DateArchived != null;
    }

    public virtual bool IsInactive()
    {
        return LockoutEnd > DateTime.UtcNow.ToLocalTime();
    }

    public virtual bool IsLocked()
    {
        return LockoutEnd != null && LockoutEnd > DateTime.UtcNow.ToLocalTime();
    }

    public virtual bool IsSuper()
    {
        if (SuperUserExpiration == null)
        {
            return false;
        }

        return DateTime.UtcNow < SuperUserExpiration;
    }
}