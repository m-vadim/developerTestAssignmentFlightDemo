namespace FlySearch.AirwaysApi.Airways.HotAir.Domain;

public sealed class FlightDescription {
	public FlightDescription(string flightNumber, Departure departure, Arrival arrival, Seat[] seatsAvailable) {
		FlightNumber = flightNumber;
		Departure = departure;
		Arrival = arrival;
		SeatsAvailable = seatsAvailable;
	}

	public string FlightNumber { get; }
	public Departure Departure { get; }
	public Arrival Arrival { get; }
	public Seat[] SeatsAvailable { get; }
}
