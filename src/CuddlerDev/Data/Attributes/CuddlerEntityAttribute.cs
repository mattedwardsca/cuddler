namespace CuddlerDev.Data.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CuddlerEntityAttribute : Attribute
{
    private readonly CuddlerEntityTargets _attributeTarget;

    public CuddlerEntityAttribute(CuddlerEntityTargets validOn)
    {
        _attributeTarget = validOn;
    }

    public bool CanBeOrganizationAdmin => _attributeTarget.HasFlag(CuddlerEntityTargets.OrganizationAdmin);

    public bool CanBeOrganizationMember => _attributeTarget.HasFlag(CuddlerEntityTargets.OrganizationMember);
}

[Flags]
public enum CuddlerEntityTargets
{
    OrganizationAdmin = 0x0001,
    OrganizationMember = 0x0002,
    WebsiteAdmin = 0x0003,

    All = OrganizationAdmin | OrganizationMember | WebsiteAdmin
}