namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FindFlightsQuery {
	public DateTimeOffset? FlightDate { get; init; }
	public string? FlightNumber { get; init; }
	public string? Destination { get; init; }
	public string? Origin { get; init; }
	public string? SortBy { get; set; }
}
