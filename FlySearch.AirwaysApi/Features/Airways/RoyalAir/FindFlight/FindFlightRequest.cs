using Microsoft.AspNetCore.Mvc;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.FindFlight;

public sealed class FindFlightRequest {
	[FromQuery]
	public DateTimeOffset? FlightDate { get; init; }

	[FromQuery]
	public string? FlightNumber { get; init; }

	[FromQuery]
	public string? Destination { get; init; }

	[FromQuery]
	public string? Origin { get; init; }
}
