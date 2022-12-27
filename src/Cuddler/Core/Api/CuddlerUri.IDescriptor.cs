namespace Cuddler.Core.Api;

public interface IDescriptor
{
    void Deserialize(string source);

    string Serialize();
}