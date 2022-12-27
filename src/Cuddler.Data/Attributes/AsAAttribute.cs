using Cuddler.Data.Helpers;

namespace Cuddler.Data.Attributes;

public class AsAAttribute : Attribute
{
    public AsAAttribute(EStoryRole actor)
    {
        Actor = actor;
    }

    public EStoryRole Actor { get; }
}