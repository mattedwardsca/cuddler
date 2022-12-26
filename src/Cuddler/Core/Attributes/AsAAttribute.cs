using Cuddler.Core.Data;

namespace Cuddler.Core.Attributes;

public class AsAAttribute : Attribute
{
    public AsAAttribute(EStoryRole actor)
    {
        Actor = actor;
    }

    public EStoryRole Actor { get; }
}