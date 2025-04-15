namespace FlySearch.ConsoleClient.Application;

public sealed class Flight {
	public required string Airline { get; init; } = string.Empty;
	public required string FlightNumber { get; init; }
	public string ArrivalAirportCode { get; init; } = string.Empty;
	public DateTimeOffset ArrivalTime { get; init; }
	public string DepartureAirportCode { get; init; } = string.Empty;
	public DateTimeOffset DepartureTime { get; init; }
	public Seat[] Seats { get; init; } = [];
	public required DateOnly FlightDate { get; init; }

	public override string ToString() {
		return $"Flight {FlightNumber} ({Airline}) from {DepartureAirportCode} to {ArrivalAirportCode} on {FlightDate:yyyy-MM-dd} " +
		       $"departing at {DepartureTime:HH:mm} and arriving at {ArrivalTime:HH:mm}.";
	}
}
