using CuddlerDev.Utils;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerKanban;

public class FactoryBoardModel
{
    public FactoryBoardModel(string name, string id, List<StationModel> stations)
    {
        Id = id;
        Name = name;
        Stations = stations;
    }

    public string Id { get; set; }

    public virtual string LinkName => LinkUtil.ToMenuLink(Name);

    public string Name { get; set; }

    public List<StationModel> Stations { get; set; }
}