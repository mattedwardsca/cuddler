using Cuddler.Pages.Shared.Cuddler.CuddlerKanban;

namespace Cuddler.Core.Projects;

public static class DealsUtil
{
    public const string DealsBoardId = "0d8eeea1-83c8-4e12-9e50-b3e840ea6248";

    public static StationModel GetFirstStation()
    {
        return ListStations()
            .First();
    }

    public static FactoryBoardModel GetDealsBoard()
    {
        var stations = ListStations();

        return new FactoryBoardModel("Deals", DealsBoardId, stations);
    }

    private static List<StationModel> ListStations()
    {

        var stations = new List<StationModel>
        {
            new("Qualified", EStationType.LightBlue),
            new("Contact Made", EStationType.DarkBlue),
            new("Meeting Scheduled", EStationType.DarkBlue),
            new("Proposal Made", EStationType.DarkBlue),
            new("Negotiations Started", EStationType.Green)
        };
        return stations;
    }
}