namespace Cuddler.Pages.Shared.Cuddler.CuddlerKanban;

public class StationModel
{
    public StationModel(string name, EStationType stationType)
    {
        Name = name;
        StationType = stationType;
    }

    public string Name { get; set; }

    public EStationType StationType { get; set; }
}