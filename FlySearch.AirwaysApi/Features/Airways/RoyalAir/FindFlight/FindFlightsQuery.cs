namespace FlySearch.AirwaysApi.Airways.RoyalAir.FindFlight;

public sealed class FindFlightsQuery {
	public DateTimeOffset? FlightDate { get; init; }
	public string? FlightNumber { get; init; }
	public string? Destination { get; init; }
	public string? Origin { get; init; }
}
