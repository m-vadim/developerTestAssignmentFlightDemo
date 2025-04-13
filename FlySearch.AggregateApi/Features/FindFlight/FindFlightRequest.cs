﻿using Microsoft.AspNetCore.Mvc;

namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FindFlightRequest {
	[FromQuery]
	public DateTimeOffset? FlightDate { get; init; }

	[FromQuery]
	public string? FlightNumber { get; init; }

	[FromQuery]
	public string? Destination { get; init; }

	[FromQuery]
	public string? Origin { get; init; }

	[FromQuery]
	public string? SortBy { get; init; }
}
