namespace Cuddler.Data.Attributes;

/// <summary>
///     Tells Cuddler the name of the token to use when generating tokens for a new record
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class TokenNameAttribute : Attribute
{
    public TokenNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}