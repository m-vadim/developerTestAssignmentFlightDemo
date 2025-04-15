namespace FlySearch.AggregateApi.Features.BookFlight;

public sealed class BookFlightRequest {
	public required string Airline { get; init; }
	public required string FlightNumber { get; init; }
	public required string SeatNumber { get; init; }
	public required string Username { get; init; }
}
