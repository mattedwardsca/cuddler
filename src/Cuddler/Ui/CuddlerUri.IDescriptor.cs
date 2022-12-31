namespace Cuddler.Forms.Ui;

public interface IDescriptor
{
    void Deserialize(string source);

    string Serialize();
}