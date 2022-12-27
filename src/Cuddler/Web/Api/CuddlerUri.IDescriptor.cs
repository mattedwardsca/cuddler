namespace Cuddler.Web.Api;

public interface IDescriptor
{
    void Deserialize(string source);

    string Serialize();
}