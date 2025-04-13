namespace FlySearch.AirwaysApi.Airways.HotAir.BookFlight;

public sealed class BookFlightRequest {
	public required string FlightNumber { get; init; }
	public required string SeatNumber { get; init; }
	public required string Username { get; init; }
}
