namespace Cuddler.Data.Entities;

public interface IHasFormModel
{
    string? SerializedModel { get; set; }

    string? SerializedType { get; set; }

    string? ModelDetails { get; set; }

    IFormModel? DeserializeModel();

    // List<FormField> ListDeserializeModelFormFields();

    void SerializeModel(object obj);
}