using CuddlerDev.Data.Helpers;

namespace CuddlerDev.Data.Attributes;

public class AsAAttribute : Attribute
{
    public AsAAttribute(EStoryRole actor)
    {
        Actor = actor;
    }

    public EStoryRole Actor { get; }
}