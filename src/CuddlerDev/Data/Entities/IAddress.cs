namespace CuddlerDev.Data.Entities;

public interface IAddress
{
    string? City { get; set; }

    double DistanceFromStore { get; set; }

    string Id { get; set; }

    double Latitude { get; set; }

    double Longitude { get; set; }

    string? PostalCode { get; set; }

    string? ProvinceCode { get; set; }

    string? RegionId { get; set; }

    string? Street1 { get; set; }

    string? Street2 { get; set; }

    string FormatAddress();
}