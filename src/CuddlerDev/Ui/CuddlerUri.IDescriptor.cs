namespace CuddlerDev.Ui;

public interface IDescriptor
{
    void Deserialize(string source);

    string Serialize();
}